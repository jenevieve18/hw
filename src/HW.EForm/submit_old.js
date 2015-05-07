window.history.forward(1);

function closeWindow(prmpt)
{
	if(prmpt == '' || confirm(prmpt))
	{
		window.open('','_parent','');
		window.close();
	}
}

function fixedButtonsInit()
{
	if(document.all && !window.opera)
	{
		document.getElementById('fixedButtons').style.position='absolute';
		fixedButtonsUpdate();
	}
	else
	{
		document.getElementById('fixedButtons').style.position='fixed';
	}
}

function fixedButtonsUpdate()
{
	if(document.all && !window.opera)
	{
		document.getElementById('fixedButtons').style.top=document.body.scrollTop+155;
	}
}

function VAStoggle(t)
{
		eval('document.forms[0].VASvalue'+t+'.value=\'NULL\'');
		document.getElementById('VASknob'+t).style.visibility='hidden';
}
		
function updateVAS(e)
{
	var eObj, sr;
	
	if(document.all)
	{
		eObj = window.event;
		sr = eObj.srcElement;
	}
	else
	{
		eObj = e;
		sr = eObj.target;
	}
	
	if(document.forms[0].VASinactive.value != '1')
	{
		if(sr.id.indexOf('VASl') >= 0)
		{
   			var vasid = sr.id.substring(4,sr.id.length);
			var xCoord = 0;
			while (sr)
			{
				xCoord += sr.offsetLeft;
				sr = sr.offsetParent;
			}
			var tObj = document.getElementById('VASk'+vasid);
			var val = eObj.clientX - 2 + document.documentElement.scrollLeft;
			tObj.style.left = val + 'px';
			val=Math.abs(Math.round((val-xCoord)/3.3));
			if(val<0)
				val=0;
			else if(val>100)
				val=100;
			var VASvalue = eval('document.forms[0].'+vasid+'.value');
			hasval = (VASvalue!='NULL' && VASvalue!='');
			if(!hasval)
			{
				tObj.style.visibility = 'visible';
				updateButtons(1,(parseInt(eval('document.forms[0].VASf'+vasid+'.value')) == 1));
			}
			eval('document.forms[0].'+vasid+'.value=val;');
		}
	}
}

function updateVASold(e)
{
	var eObj, sr;
	
	if(document.all)
	{
		eObj = window.event;
		sr = eObj.srcElement;
	}
	else
	{
		eObj = e;
		sr = eObj.target;
	}
	
	if(document.forms[0].VASinactive.value != '1')
	{
		if(sr.id.indexOf('VASline') >= 0)
		{
   			var vasnr = sr.id.substring(7,sr.id.length);
			var xCoord = 0;
			while (sr)
			{
				xCoord += sr.offsetLeft;
				sr = sr.offsetParent;
			}
			var tObj = document.getElementById('VASknob'+vasnr);
			var val = eObj.clientX - 2 + document.documentElement.scrollLeft;
			tObj.style.left = val + 'px';
			val=Math.abs(Math.round((val-xCoord)/3.3));
			if(val<0)
				val=0;
			else if(val>100)
				val=100;
			var VASvalue = eval('document.forms[0].VASvalue'+vasnr+'.value');
			hasval = (VASvalue!='NULL' && VASvalue!='');
			if(!hasval)
			{
				tObj.style.visibility = 'visible';
				updateButtons(1,(parseInt(eval('document.forms[0].VASforced'+vasnr+'.value')) == 1));
			}
			eval('document.forms[0].VASvalue'+vasnr+'.value=val;');
		}
	}
}

function loadVAS()
{
	var VASarr = document.forms[0].VASarr.value.split(',');
	for(int i=0; i<VASarr.length; i++)
	{
		var VASvalue = eval('document.forms[0].'+VASarr[i]+'.value');
		var tObj = document.getElementById('VASk'+VASarr[i]);
		if(VASvalue!='NULL' && VASvalue!='')
		{
			var xCoord = 0;
			var sr = document.getElementById('VASl'+VASarr[i]);
			while (sr)
			{
				xCoord += sr.offsetLeft;
				sr = sr.offsetParent;
			}
			tObj.style.left = xCoord + Math.round(VASvalue*3.3) + 'px';
			tObj.style.visibility = 'visible';
		}
	}
}

function loadVASold()
{
	var VAScount = document.forms[0].VAScount.value;
	if(VAScount > 0)
	{
		for(i=1;i<=VAScount;i++)
		{
			var VASvalue = eval('document.forms[0].VASvalue'+i+'.value');
			var tObj = document.getElementById('VASknob'+i);
			if(VASvalue!='NULL' && VASvalue!='')
			{
				var xCoord = 0;
				var sr = document.getElementById('VASline'+i);
				while (sr)
				{
					xCoord += sr.offsetLeft;
					sr = sr.offsetParent;
				}
				tObj.style.left = xCoord + Math.round(VASvalue*3.3) + 'px';
				tObj.style.visibility = 'visible';
			}
		}
	}
}

document.onmousedown = updateVAS;

var hasval = false;

function setTxt(s)
{
	hasval = (s.length > 0);
}

function chkTxt(s,f)
{
	if(hasval && s == '')
	{
		updateButtons(-1,f);
	}
	else if(!hasval && s != '')
	{
		updateButtons(1,f);
	}
	setTxt(s);
}

function isChecked(r)
{ 
	var i = r.length;
	var c = false;
	while(i-- > 0)
	{
		c = r[i].checked;
		if(c)
		{
			break;
		}
	} 
	return c; 
}

function toggleVi(id)
{
	if(document.getElementById(id))
	{
		if(document.getElementById(id).style.display=='none')
		{
			document.getElementById(id).style.display='';
		}
		else
		{
			document.getElementById(id).style.display='none';
		}
	}
}

function vi(qi,s)
{
	if(document.getElementById('Q' + qi + 'A'))
		document.getElementById('Q' + qi + 'A').style.display = (s ? '' : 'none');
		
	if(document.getElementById('Q' + qi + 'Q'))
		document.getElementById('Q' + qi + 'Q').style.display = (s ? '' : 'none');
		
	if(document.getElementById('Q' + qi + 'H'))
		document.getElementById('Q' + qi + 'H').style.display = (s ? '' : 'none');
		
	if(document.getElementById('Q' + qi + 'S1'))
		document.getElementById('Q' + qi + 'S1').style.display = (s ? '' : 'none');
		
	if(document.getElementById('Q' + qi + 'S2'))
		document.getElementById('Q' + qi + 'S2').style.display = (s ? '' : 'none');
		
	if(document.getElementById('Q' + qi + 'S3'))
		document.getElementById('Q' + qi + 'S3').style.display = (s ? '' : 'none');
}

function isVal(r,v)
{ 
	var i = r.length;
	var c = false;
	while(i-- > 0)
	{
		c = (r[i].value == v && r[i].checked);
		if(c)
		{
			break;
		}
	} 
	return c; 
}

function setRad(r)
{
	hasval = isChecked(r);
}

function chkRad(r,f)
{
	var c = isChecked(r);
	if(hasval && !c)
	{
		updateButtons(-1,f);
	}
	else if(!hasval && c)
	{
		updateButtons(1,f);
	}
	hasval = c;
}

function updateButtons(act,f)
{
	var totl = parseInt(eval('document.forms[0].COMPLETEDcount.value'));
	var s;
	if(f)
	{
		var cmpl = parseInt(eval('document.forms[0].FORCEDcompleted.value'));
		var reqd = parseInt(eval('document.forms[0].FORCEDcount.value'));

		if(reqd == cmpl && parseInt(act) == -1)
		{
			if(s = document.getElementById('SendCtrl'))
			{
				s.src = 'submitImages/button_send0_0.gif';
			}
		}
		else if(cmpl + parseInt(act) >= reqd)
		{
			if(s = document.getElementById('SendCtrl'))
			{
				s.src = 'submitImages/button_send0_1.gif';
			}
		}
		if(parseInt(act) != 0)
		{
			eval('document.forms[0].FORCEDcompleted.value='+(cmpl+parseInt(act)));
		}
	}

	if(totl > 0 && totl + parseInt(act) == 0)
	{
		if(s = document.getElementById('SaveCtrl'))
		{
			s.src = 'submitImages/button_save0_0.gif';
		}
	}
	else if(totl + parseInt(act) > 0)
	{
		if(s = document.getElementById('SaveCtrl'))
		{
			s.src = 'submitImages/button_save0_1.gif';
		}
	}
	if(parseInt(act) != 0)
	{
		eval('document.forms[0].COMPLETEDcount.value='+(totl+parseInt(act)));
	}
}


