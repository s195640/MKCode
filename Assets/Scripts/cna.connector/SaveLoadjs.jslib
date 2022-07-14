mergeInto(LibraryManager.library, {

  LoadGameNamesJs: function() {
    let cookieNames = '';
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf('cna') == 0) {
        cookieNames += (c.split('=')[0] +";");
      }
    }
	var bufferSize = lengthBytesUTF8(cookieNames) + 1;
	var buffer = _malloc(bufferSize);
	stringToUTF8(cookieNames, buffer, bufferSize);
    return buffer;
  },

  WriteDataJs: function (name,value,days) {
	var n = UTF8ToString(name);
	var v = encodeURIComponent(UTF8ToString(value));
	var d = UTF8ToString(days);
    var expires = "";
    if (d) {
		console.log(d*24*60*60*1000);
        var date = new Date();
        date.setTime(date.getTime() + (d*24*60*60*1000));
        expires = "; expires=" + date.toUTCString();
    }
	var cookieVal = n + "=" + (v || "")  + expires + "; path=/";
    document.cookie = cookieVal;
  },
  
  LoadDataJs: function (cname) {
	let name = UTF8ToString(cname) + "=";
	let decodedCookie = decodeURIComponent(document.cookie);
	let ca = decodedCookie.split(';');
	var data = '';
    for(let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
		data = c.substring(name.length, c.length);
		var bufferSize = lengthBytesUTF8(data) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(data, buffer, bufferSize);
		return buffer;
      }
    }
    return null;
  },

  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(UTF8ToString(str));
  },

});