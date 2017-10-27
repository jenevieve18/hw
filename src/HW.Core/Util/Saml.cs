﻿/*	Jitbit's simple SAML 2.0 component for ASP.NET
	https://github.com/jitbit/AspNetSaml/
	(c) Jitbit LP, 2016
	Use this freely under the MIT license (see http://choosealicense.com/licenses/mit/)
	version 1.2
*/

using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.IO.Compression;
using System.Text;
using System.Security.Cryptography;

using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using System.Configuration;

namespace HW.Core.Util.Saml
{
    /// <summary>
    /// this class adds support of SHA256 signing to .NET 4.0 and earlier
    /// (you can use it in .NET 4.5 too, if you don't want a "System.Deployment" dependency)
    /// </summary>
    public sealed class RSAPKCS1SHA256SignatureDescription : SignatureDescription
    {
        public RSAPKCS1SHA256SignatureDescription()
        {
            KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            DigestAlgorithm = typeof(SHA256Managed).FullName;   // Note - SHA256CryptoServiceProvider is not registered with CryptoConfig
            FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
            DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
            deformatter.SetHashAlgorithm("SHA256");
            return deformatter;
        }

        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA256");
            return formatter;
        }

        private static bool _initialized = false;
        public static void Init()
        {
            if (!_initialized)
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            _initialized = true;
        }
    }

    public class Certificate
    {
        public X509Certificate2 cert;

        public void LoadCertificate(string certificate)
        {
            LoadCertificate(StringToByteArray(certificate));
        }

        public void LoadCertificate(byte[] certificate)
        {
            cert = new X509Certificate2();
            cert.Import(certificate);
        }

        private byte[] StringToByteArray(string st)
        {
            byte[] bytes = new byte[st.Length];
            for (int i = 0; i < st.Length; i++)
            {
                bytes[i] = (byte)st[i];
            }
            return bytes;
        }
    }

    public class Response
    {
        private XmlDocument _xmlDoc;
        private Certificate _certificate;
        private XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

        public string Xml { get { return _xmlDoc.OuterXml; } }

        public Response(string certificateStr)
        {
            RSAPKCS1SHA256SignatureDescription.Init(); //init the SHA256 crypto provider (for needed for .NET 4.0 and lower)

            _certificate = new Certificate();
            _certificate.LoadCertificate(certificateStr);
        }

        public void LoadXml(string xml)
        {
            _xmlDoc = new XmlDocument();
            _xmlDoc.PreserveWhitespace = true;
            _xmlDoc.XmlResolver = null;
            _xmlDoc.LoadXml(xml);

            _xmlNameSpaceManager = GetNamespaceManager(); //lets construct a "manager" for XPath queries
        }

        public void LoadXmlFromBase64(string response)
        {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            LoadXml(enc.GetString(Convert.FromBase64String(response)));
        }

        public bool IsValid()
        {
            XmlNodeList nodeList = _xmlDoc.SelectNodes("//ds:Signature", _xmlNameSpaceManager);

            SignedXml signedXml = new SignedXml(_xmlDoc);

            if (nodeList.Count == 0) return false;

            signedXml.LoadXml((XmlElement)nodeList[0]);
            return ValidateSignatureReference(signedXml) && signedXml.CheckSignature(_certificate.cert, true) && !IsExpired();
        }

        //an XML signature can "cover" not the whole document, but only a part of it
        //.NET's built in "CheckSignature" does not cover this case, it will validate to true.
        //We should check the signature reference, so it "references" the id of the root document element! If not - it's a hack
        private bool ValidateSignatureReference(SignedXml signedXml)
        {
            if (signedXml.SignedInfo.References.Count != 1) //no ref at all
                return false;

            var reference = (Reference)signedXml.SignedInfo.References[0];
            var id = reference.Uri.Substring(1);

            var idElement = signedXml.GetIdElement(_xmlDoc, id);

            if (idElement == _xmlDoc.DocumentElement)
                return true;
            else //sometimes its not the "root" doc-element that is being signed, but the "assertion" element
            {
                var assertionNode = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion", _xmlNameSpaceManager) as XmlElement;
                if (assertionNode != idElement)
                    return false;
            }

            return true;
        }

        private bool IsExpired()
        {
            DateTime expirationDate = DateTime.MaxValue;
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:SubjectConfirmation/saml:SubjectConfirmationData", _xmlNameSpaceManager);
            if (node != null && node.Attributes["NotOnOrAfter"] != null)
            {
                DateTime.TryParse(node.Attributes["NotOnOrAfter"].Value, out expirationDate);
            }
            return DateTime.UtcNow > expirationDate.ToUniversalTime();
        }

        public string GetNameID()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", _xmlNameSpaceManager);
            return node.InnerText;
        }

        public string GetEmail()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='User.email']/saml:AttributeValue", _xmlNameSpaceManager);

            //some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
            if (node == null)
                node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);

            return node == null ? null : node.InnerText;
        }

        /// <summary>
        /// Additional to get UID data from saml response
        /// </summary>
        public string GetUId()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='urn:mace:dir:attribute-def:uid']/saml:AttributeValue", _xmlNameSpaceManager);

            //some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"
            if (node == null)
                node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='urn:mace:dir:attribute-def:uid']/saml:AttributeValue", _xmlNameSpaceManager);
            return node == null ? null : node.InnerText;
        }

        public string GetAttributeValue(String value)
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='" + value + "']/saml:AttributeValue", _xmlNameSpaceManager);

            if (node == null)
                node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='" + value + "']/saml:AttributeValue", _xmlNameSpaceManager);
            return node == null ? null : node.InnerText;
        }


        public string GetFirstName()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='first_name']/saml:AttributeValue", _xmlNameSpaceManager);

            //some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"
            if (node == null)
                node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname']/saml:AttributeValue", _xmlNameSpaceManager);

            return node == null ? null : node.InnerText;
        }

        public string GetLastName()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='last_name']/saml:AttributeValue", _xmlNameSpaceManager);

            //some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"
            if (node == null)
                node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname']/saml:AttributeValue", _xmlNameSpaceManager);
            return node == null ? null : node.InnerText;
        }

        public string GetDepartment()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/department']/saml:AttributeValue", _xmlNameSpaceManager);
            return node == null ? null : node.InnerText;
        }

        public string GetPhone()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone']/saml:AttributeValue", _xmlNameSpaceManager);
            if (node == null)
                node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/telephonenumber']/saml:AttributeValue", _xmlNameSpaceManager);
            return node == null ? null : node.InnerText;
        }

        public string GetCompany()
        {
            XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/companyname']/saml:AttributeValue", _xmlNameSpaceManager);
            return node == null ? null : node.InnerText;
        }

        //returns namespace manager, we need one b/c MS says so... Otherwise XPath doesnt work in an XML doc with namespaces
        //see https://stackoverflow.com/questions/7178111/why-is-xmlnamespacemanager-necessary
        private XmlNamespaceManager GetNamespaceManager()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(_xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            return manager;
        }
    }

    public class AuthRequest
    {
        public string _id;
        private string _issue_instant;

        private string _issuer;
        private string _assertionConsumerServiceUrl;
        public string _endpoint;

        public enum AuthRequestFormat
        {
            Base64 = 1
        }

        public AuthRequest(string issuer, string assertionConsumerServiceUrl, string endPoint)
        {
            RSAPKCS1SHA256SignatureDescription.Init(); //init the SHA256 crypto provider (for needed for .NET 4.0 and lower)

            _id = "pfx" + System.Guid.NewGuid().ToString();
            _issue_instant = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

            _issuer = issuer;
            _assertionConsumerServiceUrl = assertionConsumerServiceUrl;
            _endpoint = endPoint;
        }

        public string GetRequest(AuthRequestFormat format)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;

                using (XmlWriter xw = XmlWriter.Create(sw, xws))
                {

                    xw.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");

                    //xw.WriteElementString("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteAttributeString("xmlns", "saml", null, "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteAttributeString("ID", _id);
                    xw.WriteAttributeString("Version", "2.0");
                    xw.WriteAttributeString("ProviderName", "SP test");
                    xw.WriteAttributeString("IssueInstant", _issue_instant);
                    xw.WriteAttributeString("Destination", _endpoint);
                    xw.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
                    xw.WriteAttributeString("AssertionConsumerServiceURL", _assertionConsumerServiceUrl);

                    xw.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                    //xw.WriteString("http://" + HttpContext.Current.Request.Url.Host + "/metadata.aspx");
                    xw.WriteString(_issuer);
                    xw.WriteEndElement();

                    xw.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xw.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:1.1:nameid-format:persistent");
                    xw.WriteAttributeString("AllowCreate", "true");
                    xw.WriteEndElement();

                    //xw.WriteStartElement("samlp", "RequestedAuthnContext", "urn:oasis:names:tc:SAML:2.0:protocol");
                    //xw.WriteAttributeString("Comparison", "exact");
                    //xw.WriteStartElement("saml", "AuthnContextClassRef", null);
                    ////, "urn:oasis:names:tc:SAML:2.0:assertion"
                    //xw.WriteString("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport");
                    //xw.WriteEndElement();
                    //xw.WriteEndElement();

                    xw.WriteEndElement();
                }



                var filename = ConfigurationManager.AppSettings["AuthRequestCertificateFileName"];
                var password = ConfigurationManager.AppSettings["AuthRequestCertificatePassword"];
                try
                {
                    var cert = new X509Certificate2(System.Web.HttpContext.Current.Server.MapPath(filename), password);

                    //var xmlValue = SignXmlFile(sw.ToString(), _id, cert);
                    var xmlValue = sw.ToString();
                    var signAuthRequest = ConfigurationManager.AppSettings["SignAuthRequests"];
                    if (signAuthRequest.ToLower() == "true")
                    {
                        xmlValue = SignXmlFile(sw.ToString(), _id, cert);
                    }


                    if (format == AuthRequestFormat.Base64)
                    {
                        var memoryStream = new MemoryStream();
                        var writer = new StreamWriter(new DeflateStream(memoryStream, CompressionMode.Compress, true), new UTF8Encoding(false));

                        writer.Write(xmlValue);
                        writer.Close();
                        string result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.None);
                        return result;
                    }
                }
                catch(Exception exx)
                {
                    //throw new Exception("Error in checking certificate : " + exx.Message.ToString());
                    throw new Exception(exx.Message.ToString());
                }
                

                return null;
            }
        }



        public string SignXmlFile(string xmlAuthnRequest, string pfxRef, X509Certificate2 certificate)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlAuthnRequest);
            SignedXml signedXml = new SignedXml(doc);
            signedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";

            try
            {
                signedXml.SigningKey = certificate.PrivateKey;
            }
            catch (Exception errr)
            {

                //throw new Exception("Error in this part : " + errr.Message.ToString());
                throw new Exception(errr.Message.ToString());
            }


            KeyInfo keyInfo = new KeyInfo();

            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(certificate);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;


            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            XmlDsigExcC14NTransform c14n = new XmlDsigExcC14NTransform();

            //Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "#" + pfxRef;

            reference.AddTransform(env);
            reference.AddTransform(c14n);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();
            bool checkSignature = signedXml.CheckSignature(certificate, true);

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            AssignNameSpacePrefixToElementTree(xmlDigitalSignature, "ds");


            // Append the element to the XML document.
            //doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
            var temp = doc.FirstChild.FirstChild;
            doc.DocumentElement.InsertAfter(doc.ImportNode(xmlDigitalSignature, true), temp);

            //if (doc.FirstChild is XmlDeclaration)
            //{
            //    doc.RemoveChild(doc.FirstChild);
            //}

            return doc.OuterXml;
        }

        private static void AssignNameSpacePrefixToElementTree(XmlElement element, string prefix)
        {
            element.Prefix = prefix;

            foreach (var child in element.ChildNodes)
            {
                if (child is XmlElement)
                    AssignNameSpacePrefixToElementTree(child as XmlElement, prefix);
            }
        }

        //returns the URL you should redirect your users to (i.e. your SAML-provider login URL with the Base64-ed request in the querystring
        public string GetRedirectUrl(string samlEndpoint)
        {
            var queryStringSeparator = samlEndpoint.Contains("?") ? "&" : "?";

            return samlEndpoint + queryStringSeparator + "SAMLRequest=" + HttpUtility.UrlEncode(this.GetRequest(AuthRequest.AuthRequestFormat.Base64));
        }
    }
}