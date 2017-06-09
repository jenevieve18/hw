// Email notes
// kevin@scoollabs.com
//
// Non-SSL Settings
// (NOT Recommended)
// Username: 	kevin@scoollabs.com
// Password: 	Use your cPanel password.
// Incoming Server: 	mail.scoollabs.com
//
//     IMAP Port: 143
//     POP3 Port: 110
//
// Outgoing Server: 	mail.scoollabs.com
//
//     SMTP Port: 25
//
// Authentication is required for IMAP, POP3, and SMTP.


// var Imap = require('imap'),
//     inspect = require('util').inspect;
//
// var imap = new Imap({
//   user: 'kevin@scoollabs.com',
//   password: 'KevinTheGreat!',
//   host: 'sg2plcpnl0009.prod.sin2.secureserver.net',
//   port: 993,
//   tls: true
// });

var inbox = require("inbox");
var MailParser = require("front-mailparser").MailParser;

var client = inbox.createConnection(false, "sg2plcpnl0009.prod.sin2.secureserver.net", {
    secureConnection: true,
    auth:{
        user: 'kevin@scoollabs.com',
        pass: "KevinTheGreat!"
    }
});

client.on("connect", function(){
    console.log("Successfully connected to server");
    client.listMailboxes(function(error, mailboxes){
        client.openMailbox("INBOX", function(error, info){
            if(error) throw error;
            console.log("Message count in INBOX: " + info.count);
            client.listMessages(-1, function(err, messages){ //Check the last 1 (-1) messages
                messages.forEach(function(message){
                    var mailparser = new MailParser();

                    //console.log(message.UID + ": " + message.title);
                    //console.log(JSON.stringify(message));
                    email = client.createMessageStream(message.UID).pipe(mailparser, {end: true});

                    mailparser.on("end", function(mail_object){

                    console.log("From:", mail_object.from); //[{address:'sender@example.com',name:'Sender Name'}]
                    if (mail_object.from[0].address === 'support@healthwatch.se') {console.log("Assert here")};
                    console.log("Subject:", mail_object.subject); // Hello world!
                    console.log("Text body:", mail_object.text); // How are you today?
//
                    });

                });
                client.close();
            });
        });
    })
});

client.connect();

//user: testuseremail
//password: aoeuhtns
//password after reset: aoeuhtns1
//email: kevin@scoollabs.com
//secretkey: 98bdb70131f94ccb9d5cce9327a6b0a1
//Secretkey after reset: 775c64f0db97416d93682c86d7c99575
//resourceID: 5ff64b62-049f-40b5-b400-bb751a01542b
