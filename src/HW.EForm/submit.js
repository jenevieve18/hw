window.history.forward(1);

function closeWindow(prmpt)
{
	if(prmpt == '' || confirm(prmpt))
	{
		window.open('','_parent','');
		window.close();
	}
}

function closeWindowRefreshParent(prmpt)
{
	window.opener.location.reload();
	
	if(prmpt == '' || confirm(prmpt))
	{
		window.open('','_parent','');
		window.close();
	}
}

function fixedButtonsInit()
{
	updatePage();
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
		eval('document.forms[0].'+t+'.value=\'NULL\'');
		document.getElementById('VASk'+t).style.visibility='hidden';
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
			if(val<0) val=0;
			else if(val>100) val=100;
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

function loadVAS()
{
	if(document.forms[0].VASarr.value!='')
	{
		var VASarr = document.forms[0].VASarr.value.split(',');
		for(i=0; i<VASarr.length; i++)
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
	if(!isNaN(r.length))
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
	else
	{
		return r.checked;
	}
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
	{
		if(!s && document.getElementById('Q' + qi + 'Q').style.display == '')
		{
			eval('clr' + qi + '(-1);');
		}
		else if(s && document.getElementById('Q' + qi + 'Q').style.display == 'none')
		{
			eval('clr' + qi + '(1);');
		}
		document.getElementById('Q' + qi + 'Q').style.display = (s ? '' : 'none');
	}
		
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
	var c = false;
	if(r.type=='hidden')
	{
		c = (r.value == v);
	}
	else
	{
		if(isNaN(r.length))
		{
			c = (r.value == v && r.checked);
		}
		else
		{
			var i = r.length;
			while(i-- > 0)
			{
				c = (r[i].value == v && r[i].checked);
				if(c)
				{
					break;
				}
			}
		} 
	}
	return c; 
}

function setRad(r)
{
	hasval = isChecked(r);
}

function clrRad(r)
{
	if(isNaN(r.length))
	{
		r.checked = false;
	}
	else
	{
		var i = r.length;
		while(i-- > 0)
		{
			r[i].checked = false;
		} 
	}
}

function manTxt(s,f,n)
{
	chgCnt(f,n);
	if(n == -1 && eval('document.forms[0].'+s+'.value') != '')
	{
		eval('document.forms[0].'+s+'.value=\'\';');
		if(document.getElementById('VASk'+s))
			document.getElementById('VASk'+s).style.visibility = 'hidden';
		updateButtons(n,f);
	}
	else
	{
		updateButtons(0,f);
	}
}

function manRad(r,f,n)
{
	chgCnt(f,n);
	if(n == -1 && isChecked(r))
	{
		clrRad(r);
		updateButtons(n,f);
	}
	else
	{
		updateButtons(0,f);
	}
}

function chgCnt(f,n)
{
	var tCX = parseInt(eval('document.forms[0].tCX.value'));
	eval('document.forms[0].tCX.value='+(tCX+n));
	if(f)
	{
		var fCX = parseInt(eval('document.forms[0].FORCEDcount.value'));
		eval('document.forms[0].FORCEDcount.value='+(fCX+n));
	}
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
	var tCX = parseInt(eval('document.forms[0].tCX.value'));
	var tcCX = parseInt(eval('document.forms[0].tcCX.value'));
	var a = parseInt(act);
	
	var s;
	if(f)
	{
		var cmpl = parseInt(eval('document.forms[0].FORCEDcompleted.value')) + a;
		var reqd = parseInt(eval('document.forms[0].FORCEDcount.value'));

		if(reqd > cmpl)
		{
			if(s = document.getElementById('SendCtrl'))
			{
				s.src = 'submitImages/button_send0.gif';
			}
			else if(s = document.getElementById('NextCtrl'))
			{
				s.src = 'submitImages/button_next0.gif';
			}
		}
		else if(cmpl >= reqd)
		{
			if(s = document.getElementById('SendCtrl'))
			{
				s.src = 'submitImages/button_send1.gif';
			}
			else if(s = document.getElementById('NextCtrl'))
			{
				s.src = 'submitImages/button_next1.gif';
			}
		}
		if(a != 0)
		{
			eval('document.forms[0].FORCEDcompleted.value='+cmpl);
		}
	}

	if(a != 0)
	{
		eval('document.forms[0].tcCX.value='+(tcCX+a));
	}
	var st = (tCX == 0 ? 100 : parseInt((tcCX+a)/tCX*100));
	var t = 7;
	if(tCX < 20)
	{
		t = 0;
	}
	var n = 'submitImages/' + (st <= 0 ? 'null' : 'progress/progress_bar_' + (st > 100 ? 100 : st)) + '.gif';
	if(document.getElementById('progressBar').src != n)
	{
		document.getElementById('progressBar').src = n;
		var m = (tCX-(tcCX+a))*t;
		if(m == 0 || st == 100)
		{
			document.getElementById('minutes').innerHTML = '';
		}
		else if(m > 55)
		{
			m = parseInt(m/60);
			if(m > 1)
			{
				document.getElementById('minutes').innerHTML = 'Ca. ' + m + ' minuter kvar';
			}
			else
			{
				document.getElementById('minutes').innerHTML = 'Ca. 1 minut kvar';
			}
		}
		else
		{
			document.getElementById('minutes').innerHTML = 'Ca. ' + m + ' sekunder kvar';
		}
	}
}

function updatePage()
{
	var cp = parseInt(eval('document.forms[0].CP.value'));
	var pCX = parseInt(eval('document.forms[0].pCX.value'));
	if(cp<=pCX)
	{
		if(cp==pCX && cp > 1)
		{
			document.getElementById('status').innerHTML = 'Sista sidan!';
		}
		else
		{
			document.getElementById('status').innerHTML = 'Sidan ' + cp + ' av ' + pCX;
		}
	}
	else
	{
		document.getElementById('status').innerHTML = '';
	}
}