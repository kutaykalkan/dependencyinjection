var DefaultNumberDecimalSeparator = ".";
var NumberDecimalSeparator = ".";
var DateValidateRegExpStr = "^(?=\d)(?:(?:(?:(?:(?:0?[13578]|1[02])(\/)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:0?2(\/)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))(\/)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2}))($|\ (?=\d)))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$";

function IsPositiveDecimalOrZero(oTextBox) {
    // Check for Valid Number
    // Check for number >= 0    

    try {
        var bIsPositiveDecimalOrZero = false;
        // Validate whether it is a Number or not	    
        if (IsNumber(oTextBox)) {
            // If the above function returned true, means the Value entered is a valid Number		    
            var val = ConvertToEnglishNumber(oTextBox.value);
            if (val >= 0) {
                bIsPositiveDecimalOrZero = true;
            }
        }
        return bIsPositiveDecimalOrZero;
    }
    catch (e) {
        alert("ERROR -> IsPositiveDecimalOrZero: " + e.message);
    }
}

function IsPositiveDecimal(oTextBox) {
    // Check for Valid Number
    // Check for number > 0    

    try {
        var bIsPositiveDecimal = false;
        // Validate whether it is a Number or not	    
        if (IsNumber(oTextBox)) {
            // If the above function returned true, means the Value entered is a valid Number		    
            var val = ConvertToEnglishNumber(oTextBox.value);
            if (val > 0) {
                bIsPositiveDecimal = true;
            }
        }
        return bIsPositiveDecimal;
    }
    catch (e) {
        alert("ERROR -> IsPositiveDecimal: " + e.message);
    }
}

function IsPositiveIntegerOrZero(oTextBox) {
    // Check for Valid Number
    // Check for Integer and Integer >= 0    

    try {
        var bIsPositiveIntegerOrZero = false;
        if (IsInteger(oTextBox)) {
            // If the above function returned true, means the Value entered is a valid Integer
            var val = ConvertToEnglishNumber(oTextBox.value);
            if (val >= 0) {
                bIsPositiveIntegerOrZero = true;
            }
        }

        return bIsPositiveIntegerOrZero;
    }
    catch (e) {
        alert("ERROR -> IsPositiveIntegerOrZero: " + e.message);
    }
}

function IsPositiveInteger(oTextBox) {
    // Check for Valid Number
    // Check for Integer and Integer > 0    

    try {
        var bIsPositiveInteger = false;
        if (IsInteger(oTextBox)) {
            // If the above function returned true, means the Value entered is a valid Integer
            var val = ConvertToEnglishNumber(oTextBox.value);
            if (val > 0) {
                bIsPositiveInteger = true;
            }
        }

        return bIsPositiveInteger;
    }
    catch (e) {
        alert("ERROR -> IsPositiveInteger: " + e.message);
    }
}


function IsInteger(oTextBox) {
    try {
        var bIsInteger = false;
        // Chceck for Valid Number	    
        if (IsNumber(oTextBox)) {
            // True = Valid Number
            if (oTextBox.value.indexOf(NumberDecimalSeparator) == -1) {
                // True = No Decimal Place 
                bIsInteger = true;
            }
        }
        return bIsInteger;
    }
    catch (e) {
        alert("ERROR -> IsInteger: " + e.message);
    }
}


function IsNumber(oTextBox) {
    try {
        var bIsNumber = false;
        if (oTextBox.value != "") {
            var val = ConvertToEnglishNumber(oTextBox.value);
            if (!isNaN(val)) {
                bIsNumber = true;
            }
        }
        return bIsNumber;
    }
    catch (e) {
        alert("ERROR -> IsNumber: " + e.message);
    }
}

function ConvertToEnglishNumber(number) {
    var engNumber;

    if (NumberDecimalSeparator != ".") {
        if (number.indexOf(".") > -1)
            engNumber = "NaN";
        else
            var engNumber = number.replace(NumberDecimalSeparator, ".");
    }
    else {
        engNumber = number;
    }
    return engNumber;
}

function ValidateNumbers(sender, args) {

    var txtAmount = document.getElementById(sender.controltovalidate);
    args.IsValid = IsNumber(txtAmount);

}


//BEGIN *********** Function to Round off values 
function Round(value, noOfDecimalPlaces) {
    var o = igedit_number(value, numberProperty);
    var num = o.toNum(value);
    return RoundOffValues(num, noOfDecimalPlaces);

    ////	if ( typeof(value) == "string")
    ////		value = value.replace(",", "") ; // replace the , sign in number otherwise parseFloat will not work
    //	value = ConvertToEnglishNumber(value) ;
    //	if (noOfDecimalPlaces == 0) 
    //		return Math.round(value);
    //	else
    //	{
    //		multiplier = Math.pow(10, noOfDecimalPlaces) ;
    //		return Math.round(value * multiplier) / multiplier ;
    //	}
}


//BEGIN *********** Function to Round off values 
function RoundNumber(value, noOfDecimalPlaces) {
    var o = igedit_number(value, numberProperty);
    var num = o.toNum(value);
    return RoundOffValues(num, noOfDecimalPlaces);

    ////	if ( typeof(value) == "string")
    ////		value = value.replace(",", "") ; // replace the , sign in number otherwise parseFloat will not work
    //	value = ConvertToEnglishNumber(value) ;
    //	if (noOfDecimalPlaces == 0) 
    //		return Math.round(value);
    //	else
    //	{
    //		multiplier = Math.pow(10, noOfDecimalPlaces) ;
    //		return Math.round(value * multiplier) / multiplier ;
    //	}
}

//*********** Function to Round off values 
function RoundOffValues(value, noOfDecimalPlaces) {
    if (typeof (value) == "string")
        value = value.replace(",", ""); // replace the , sign in number otherwise parseFloat will not work
    value = parseFloat(value);
    if (noOfDecimalPlaces == 0)
        return Math.round(value);
    else {
        multiplier = Math.pow(10, noOfDecimalPlaces);
        return Math.round(value * multiplier) / multiplier;
    }
}

//END *************

//BEGIN *********** Function to Get Formatted NUmbers for Display
function GetDisplayIntegerValue(val) {
    var o = igedit_number(val, numberProperty);
    return o.toNum(val)
}

function GetDisplayDecimalValue(val, minDecimals) {
    var o = igedit_number(val, numberProperty);
    if (typeof (minDecimals) != "undefined" && !isNaN(minDecimals)) {
        o.minDecimals = minDecimals;
    }
    o.value = o.toNum(o.elem.value, true, true);
    return o.toTxt(o.value, false);
}

function GetDisplayDecimalValueForTextBox(val, minDecimals) {
    var o = igedit_number(val, numberProperty);
    if (typeof (minDecimals) != "undefined" && !isNaN(minDecimals)) {
        o.minDecimals = minDecimals;
    }
    o.value = o.toNum(o.elem.value, true, true);
    return o.toTxtBox(o.value, false);
}

//END*********** Function to Get Formatted NUmbers for Display

var NumberFormat = "#,###.00"
function FormatNumber(value) {
    // Need to put in code to Format the Number properly
    // using , and making sure its right aligned with 2 decimal places
    value = Round(value, 2);
    return NumberFormatter(value, NumberFormat);
}


if (typeof (igedit_all) != "object")
    var igedit_all = new Object();

var numberProperty = ["", "..", ",", "-", "", "", "n", "-n", 1, 3, 0, 1, 1, 1, 1000000000, "", "", 3, 2]


function igedit_number(elem, prop1) {
    var i = 1, me = new igedit_new(elem);
    var j = -1, v = me.valI(prop1, i++);
    var n = v.length; if (n < 1) v = "."; if (n < 2) { n = 2; v += v; }
    me.dec_vld = v.substring(n >>= 1);
    me.decimalSeparator = v.substring(0, n);
    me.groupSeparator = me.valI(prop1, i++);
    v = me.valI(prop1, i++);
    if (v.length < 1) v = "-";
    me.minus = v;
    //alert("me.minus=" + me.minus);
    me.symbol = me.valI(prop1, i++);
    //alert("me.symbol=" + me.symbol);
    me.nullText = me.valI(prop1, i++);
    //alert("me.nullText=" + me.nullText);

    me.positivePattern = me.valI(prop1, i++);
    //alert("me.positivePattern=" + me.positivePattern);
    me.negativePattern = me.valI(prop1, i++);
    //alert("me.negativePattern=" + me.negativePattern);
    me.mode = me.valI(prop1, i++);
    //alert("me.mode=" + me.mode);
    me.decimals = me.valI(prop1, i++);
    //alert("me.decimals=" + me.decimals);
    me.minDecimals = me.valI(prop1, i++);
    //alert("me.minDecimals=" + me.minDecimals);
    v = me.valI(prop1, i++);
    if (v == 1) me.min = me.valI(prop1, i++);
    //alert("me.min=" + me.min);
    v = me.valI(prop1, i++);
    if (v == 1) me.max = me.valI(prop1, i++);
    //alert("me.max=" + me.max);
    me.clr1 = me.valI(prop1, i++);
    //alert("me.clr1=" + me.clr1);
    me.clr0 = me.valI(prop1, i++);
    //alert("me.clr0=" + me.clr0);
    me.groups = new Array();
    while (++j < 6) { if ((v = me.valI(prop1, i++)) > 0) me.groups[j] = v; else break; }
    me.getMaxValue = function() { return this.max; }
    me.setMaxValue = function(v) { this.max = this.toNum(v, false); }
    me.getMinValue = function() { return this.min; }
    me.setMinValue = function(v) { this.min = this.toNum(v, false); }
    me.toNum = function(t, limit, fire) {
        var c, num = null, i = -1, div = 1, dec = -1, iLen = 0;
        if (t === "") t = null;
        if (t == null || t.length == null) num = t;
        else {
            var neg = false, dot = this.decimalSeparator.charCodeAt(0);
            if (t) {
                c = this.symbol;
                if (c.length > 0) if ((iLen = t.indexOf(c)) >= 0) t = t.substring(0, iLen) + t.substring(iLen + c.length);
                if (t.toUpperCase == null) t = t.toString();
                iLen = t.length;
            }

            while (++i < iLen) {
                if (this.isMinus(c = this.jpn(t.charCodeAt(i)))) { if (neg) break; neg = true; }
                if (c == dot || c == 12290) { if (dec >= 0) break; dec = 0; }
                if (c < 48 || c > 57) continue;
                if (num == null) num = 0;
                if (dec < 0) num = num * 10 + c - 48;
                else { dec = dec * 10 + c - 48; div *= 10; }
            }

            if (num != null) { if (dec > 0) num += dec / div; if (neg) num = -num; }
        }
        return num;
    }

    me.focusTxt = function(foc, e) {
        if (e != null && !foc) this.value = this.toNum(this.elem.value, true, true);
        return this.toTxt(this.value, foc);
    }

    me.toTxt = function(v, foc, t, m, dec) {
        if (t == null) {
            if (v == null) return foc ? "" : this.nullText;
            var neg = (v < 0);
            if (neg) v = -v;
            try { t = v.toFixed(this.decimals); } catch (ex) { t = "" + v; }
            return this.toTxt(neg, foc, t.toUpperCase(), (m == null) ? this.minus : m, (dec == null) ? this.decimalSeparator : dec);
        }
        var c, i = -1, iL = t.length;
        if (v == null) {
            if (iL == 0) return foc ? t : this.nullText;
            if (v = this.isMinus(t.charCodeAt(0))) t = t.substring(1);
        }
        var iE = t.indexOf("E");
        if (iE < 0) iE = 0;
        else {
            iL = parseInt(t.substring(iE + 1));
            t = t.substring(0, iE);
            iE = iL;
        }
        iL = t.length;
        while (++i < iL)//remove dot
        {
            c = t.charCodeAt(i);
            if (c < 48 || c > 57) { t = t.substring(0, i) + t.substring(i + 1); iL--; break; }
        }
        //if dot,remove trailing 0s
        while (i < iL) { if (t.charCodeAt(iL - 1) != 48) break; t = t.substring(0, --iL); }
        if (iE != 0) {
            while (iE-- > 0) if (i++ >= iL) t += "0";
            if (++iE < 0) { if (i == 0) t = "0" + t; while (++iE < 0) t = "0" + t; t = "0" + t; i = 1; }
        }
        iL = i;
        var iDec = 0;
        if (this.decimals > 0 && iL < t.length) {
            iDec = t.length - iL;
            t = t.substring(0, iL) + dec + t.substring(iL);
            iL += dec.length + this.decimals;
        }
        if (iL < t.length) t = t.substring(0, iL);
        if ((iL = this.minDecimals) != 0) { if (iDec == 0) t += dec; while (iL-- > iDec) t += "0"; }
        if (foc) return v ? (m + t) : t;
        var g0 = (this.groups.length > 0) ? this.groups[0] : 0;
        var ig = 0, g = g0;
        while (g > 0 && --i > 0) if (--g == 0) {
            t = t.substring(0, i) + this.groupSeparator + t.substring(i);
            g = this.groups[++ig];
            if (g == null || g < 1) g = g0;
            else g0 = g;
        }
        var txt = v ? this.negativePattern : this.positivePattern;
        txt = txt.replace("$", me.symbol);
        return txt.replace("n", t);
    }

    me.toTxtBox = function(v, foc, t, m, dec) {
        if (t == null) {
            if (v == null) return foc ? "" : this.nullText;
            var neg = (v < 0);
            if (neg) v = -v;
            try { t = v.toFixed(this.decimals); } catch (ex) { t = "" + v; }
            return this.toTxtBox(neg, foc, t.toUpperCase(), (m == null) ? this.minus : m, (dec == null) ? this.decimalSeparator : dec);
        }
        var c, i = -1, iL = t.length;
        if (v == null) {
            if (iL == 0) return foc ? t : this.nullText;
            if (v = this.isMinus(t.charCodeAt(0))) t = t.substring(1);
        }
        var iE = t.indexOf("E");
        if (iE < 0) iE = 0;
        else {
            iL = parseInt(t.substring(iE + 1));
            t = t.substring(0, iE);
            iE = iL;
        }
        iL = t.length;
        while (++i < iL)//remove dot
        {
            c = t.charCodeAt(i);
            if (c < 48 || c > 57) { t = t.substring(0, i) + t.substring(i + 1); iL--; break; }
        }
        //if dot,remove trailing 0s
        while (i < iL) { if (t.charCodeAt(iL - 1) != 48) break; t = t.substring(0, --iL); }
        if (iE != 0) {
            while (iE-- > 0) if (i++ >= iL) t += "0";
            if (++iE < 0) { if (i == 0) t = "0" + t; while (++iE < 0) t = "0" + t; t = "0" + t; i = 1; }
        }
        iL = i;
        var iDec = 0;
        if (this.decimals > 0 && iL < t.length) {
            iDec = t.length - iL;
            t = t.substring(0, iL) + dec + t.substring(iL);
            iL += dec.length + this.decimals;
        }
        if (iL < t.length) t = t.substring(0, iL);
        if ((iL = this.minDecimals) != 0) { if (iDec == 0) t += dec; while (iL-- > iDec) t += "0"; }
        if (foc) return v ? (m + t) : t;
        var g0 = (this.groups.length > 0) ? this.groups[0] : 0;
        var ig = 0, g = g0;
        while (g > 0 && --i > 0) if (--g == 0) {
            t = t.substring(0, i) + t.substring(i);
            g = this.groups[++ig];
            if (g == null || g < 1) g = g0;
            else g0 = g;
        }
        var txt = v ? this.negativePattern : this.positivePattern;
        txt = txt.replace("$", me.symbol);
        return txt.replace("n", t);
    }
    me.isMinus = function(k) { return k == this.minus.charCodeAt(0) || k == 45 || k == 40 || k == 12540; }

    me.limits = function(v, r) {
        if (v == null && !this.nullable) v = 0;
        if (v != null) {
            var n = this.min, x = this.max;
            if (n != null && v <= n) return r ? x : n;
            if (x != null && v >= x) return r ? n : x;
        }
        return v;
    }
    return me;
}

function igedit_new(elem, id) {
    try {
        this.fcs = -1;
        this.valI = function(o, i) { o = (o == null || o.length <= i) ? null : o[i]; return (o == null) ? "" : o; }
        this.intI = function(o, i) { return ig_csom.isEmpty(o = this.valI(o, i)) ? -1 : parseInt(o); }

        this.elemID = -10;
        this.bad = 0;
        elem.Object = this;

        var Element = new Object()
        Element.value = elem
        this.elem = Element;

        this.ID = id;

        this.jpn = function(k) { return (this.sTxt == 1 && k > 65295 && k < 65306) ? (k - 65248) : k; }
    }
    catch (e) {
        //alert("AAA:\n" + e.message)
    }
}

function IsDate(obj) {
    var oDateValidateRegExp = new RegExp(DateValidateRegExpStr);

    if (oDateValidateRegExp.exec(obj) != null)
        return true;
    else
        return false;
}

function CompareDates(sourceObject, objectToCompare) {
    var d1 = getDateFromFormat(sourceObject);
    var d2 = getDateFromFormat(objectToCompare);

    if (d1 == 0 || d2 == 0) {
        return -4;
    }
    else if (d1 > d2) {
        return 1;
    }
    else if (d1 < d2) {
        return -1;
    }
    return 0;
}

function ConvertJavascriptDateFormat(obj) {
    return dateFormat(obj, DATE_FORMAT);
}

function GetDateBasedOnDateFormat(val) {
    var format = DATE_FORMAT;
    //alert("DATE_FORMAT = " + DATE_FORMAT);
    val = val + "";
    format = format + "";
    var i_val = 0;
    var i_format = 0;
    var c = "";
    var token = "";
    var token2 = "";
    var x, y;
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = 1;
    var hh = now.getHours();
    var mm = now.getMinutes();
    var ss = now.getSeconds();
    var ampm = "";

    while (i_format < format.length) {
        // Get next token from format string
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == "yyyy" || token == "yy" || token == "y") {
            if (token == "yyyy") { x = 4; y = 4; }
            if (token == "yy") { x = 2; y = 2; }
            if (token == "y") { x = 2; y = 4; }
            year = _getInt(val, i_val, x, y);
            if (year == null) { return 0; }
            i_val += year.length;
            if (year.length == 2) {
                if (year > 70) { year = 1900 + (year - 0); }
                else { year = 2000 + (year - 0); }
            }
        }
        else if (token == "MMM" || token == "NNN") {
            month = 0;
            for (var i = 0; i < MONTH_NAMES.length; i++) {
                var month_name = MONTH_NAMES[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                    if (token == "MMM" || (token == "NNN" && i > 11)) {
                        month = i + 1;
                        if (month > 12) { month -= 12; }
                        i_val += month_name.length;
                        break;
                    }
                }
            }
            if ((month < 1) || (month > 12)) { return 0; }
        }
        else if (token == "EE" || token == "E") {
            for (var i = 0; i < DAY_NAMES.length; i++) {
                var day_name = DAY_NAMES[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == "MM" || token == "M") {
            month = _getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12)) { return 0; }
            i_val += month.length;
        }
        else if (token == "dd" || token == "d") {
            date = _getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31)) { return 0; }
            i_val += date.length;
        }
        else if (token == "hh" || token == "h") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "HH" || token == "H") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "KK" || token == "K") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "kk" || token == "k") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
            i_val += hh.length; hh--;
        }
        else if (token == "mm" || token == "m") {
            mm = _getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
            i_val += mm.length;
        }
        else if (token == "ss" || token == "s") {
            ss = _getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
            i_val += ss.length;
        }
        else if (token == "a") {
            if (val.substring(i_val, i_val + 2).toLowerCase() == "am") { ampm = "AM"; }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm") { ampm = "PM"; }
            else { return 0; }
            i_val += 2;
        }
        else {
            if (val.substring(i_val, i_val + token.length) != token) { return 0; }
            else { i_val += token.length; }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length) { return 0; }
    // Is date valid for month?
    if (month == 2) {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
            if (date > 29) { return 0; }
        }
        else { if (date > 28) { return 0; } }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
        if (date > 30) { return 0; }
    }
    // Correct hours value
    if (hh < 12 && ampm == "PM") { hh = hh - 0 + 12; }
    else if (hh > 11 && ampm == "AM") { hh -= 12; }
    var newdate = new Date(year, month - 1, date, hh, mm, ss);
    return newdate;
}


function getDateFromFormat(val) {
    var format = DATE_FORMAT;
    //alert("DATE_FORMAT = " + DATE_FORMAT);
    val = val + "";
    format = format + "";
    var i_val = 0;
    var i_format = 0;
    var c = "";
    var token = "";
    var token2 = "";
    var x, y;
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = 1;
    var hh = now.getHours();
    var mm = now.getMinutes();
    var ss = now.getSeconds();
    var ampm = "";

    while (i_format < format.length) {
        // Get next token from format string
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == "yyyy" || token == "yy" || token == "y") {
            if (token == "yyyy") { x = 4; y = 4; }
            if (token == "yy") { x = 2; y = 2; }
            if (token == "y") { x = 2; y = 4; }
            year = _getInt(val, i_val, x, y);
            if (year == null) { return 0; }
            i_val += year.length;
            if (year.length == 2) {
                if (year > 70) { year = 1900 + (year - 0); }
                else { year = 2000 + (year - 0); }
            }
        }
        else if (token == "MMM" || token == "NNN") {
            month = 0;
            for (var i = 0; i < MONTH_NAMES.length; i++) {
                var month_name = MONTH_NAMES[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                    if (token == "MMM" || (token == "NNN" && i > 11)) {
                        month = i + 1;
                        if (month > 12) { month -= 12; }
                        i_val += month_name.length;
                        break;
                    }
                }
            }
            if ((month < 1) || (month > 12)) { return 0; }
        }
        else if (token == "EE" || token == "E") {
            for (var i = 0; i < DAY_NAMES.length; i++) {
                var day_name = DAY_NAMES[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == "MM" || token == "M") {
            month = _getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12)) { return 0; }
            i_val += month.length;
        }
        else if (token == "dd" || token == "d") {
            date = _getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31)) { return 0; }
            i_val += date.length;
        }
        else if (token == "hh" || token == "h") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "HH" || token == "H") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "KK" || token == "K") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "kk" || token == "k") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
            i_val += hh.length; hh--;
        }
        else if (token == "mm" || token == "m") {
            mm = _getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
            i_val += mm.length;
        }
        else if (token == "ss" || token == "s") {
            ss = _getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
            i_val += ss.length;
        }
        else if (token == "a") {
            if (val.substring(i_val, i_val + 2).toLowerCase() == "am") { ampm = "AM"; }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm") { ampm = "PM"; }
            else { return 0; }
            i_val += 2;
        }
        else {
            if (val.substring(i_val, i_val + token.length) != token) { return 0; }
            else { i_val += token.length; }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length) { return 0; }
    // Is date valid for month?
    if (month == 2) {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
            if (date > 29) { return 0; }
        }
        else { if (date > 28) { return 0; } }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
        if (date > 30) { return 0; }
    }
    // Correct hours value
    if (hh < 12 && ampm == "PM") { hh = hh - 0 + 12; }
    else if (hh > 11 && ampm == "AM") { hh -= 12; }
    var newdate = new Date(year, month - 1, date, hh, mm, ss);
    return newdate.getTime();
}



/*
* Date Format 1.2.3
* (c) 2007-2009 Steven Levithan <stevenlevithan.com>
* MIT license
*
* Includes enhancements by Scott Trenda <scott.trenda.net>
* and Kris Kowal <cixar.com/~kris.kowal/>
*
* Accepts a date, a mask, or a date and a mask.
* Returns a formatted version of the given date.
* The date defaults to the current date/time.
* The mask defaults to dateFormat.masks.default.
*/

var dateFormat = function() {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function(val, len) {
		    val = String(val);
		    len = len || 2;
		    while (val.length < len) val = "0" + val;
		    return val;
		};

    // Regexes and supporting functions are cached through closure
    return function(date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			M = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			m = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
			    d: d,
			    dd: pad(d),
			    ddd: dF.i18n.dayNames[D],
			    dddd: dF.i18n.dayNames[D + 7],
			    M: M + 1,
			    MM: pad(M + 1),
			    MMM: dF.i18n.monthNames[M],
			    MMMM: dF.i18n.monthNames[M + 12],
			    yy: String(y).slice(2),
			    yyyy: y,
			    h: H % 12 || 12,
			    hh: pad(H % 12 || 12),
			    H: H,
			    HH: pad(H),
			    m: m,
			    mm: pad(m),
			    s: s,
			    ss: pad(s),
			    l: pad(L, 3),
			    L: pad(L > 99 ? Math.round(L / 10) : L),
			    t: H < 12 ? "a" : "p",
			    tt: H < 12 ? "am" : "pm",
			    T: H < 12 ? "A" : "P",
			    TT: H < 12 ? "AM" : "PM",
			    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
			    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
			    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

        return mask.replace(token, function($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
} ();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "M/d/yy",
    mediumDate: "MMM d, yyyy",
    longDate: "MMMM d, yyyy",
    fullDate: "dddd, MMMM d, yyyy",
    shortTime: "h:mm TT",
    mediumTime: "h:mm:ss TT",
    longTime: "h:mm:ss TT Z",
    isoDate: "yyyy-MM-dd",
    isoTime: "HH:mm:ss",
    isoDateTime: "yyyy-MM-dd'T'HH:mm:ss",
    isoUtcDateTime: "UTC:yyyy-MM-dd'T'HH:mm:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
    monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.format = function(mask, utc) {
    return dateFormat(this, mask, utc);
};

function IsAnyRadioButtonSelected(opt1ID, opt2ID) {
    var opt1 = document.getElementById(opt1ID);
    var opt2 = document.getElementById(opt2ID);
    if ((opt1 != null) && (opt2 != null)) {
        if ((opt1.checked == false) && (opt2.checked == false)) {
            return false;
        }
        else
            return true;
    }
    return false;
}

function SetDisplayDecimalValue(val, minDecimals) {
    if (val == null
        || typeof (val) == "undefined") {
        return JS_GLOBAL_CONSTANT_HYPHEN;
    }
    else {
        return GetDisplayDecimalValue(val, minDecimals);
    }
}