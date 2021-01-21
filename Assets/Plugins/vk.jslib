mergeInto(LibraryManager.library, {

/*
  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(Pointer_stringify(str));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },
*/

	
	
	_GetURLParams: function() {
		//window.alert(document.location.href+'');
		
		var returnStr = document.location.href+'';
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;		
		
	},

	_VK_init: function() {
		
		JS_VK_start();
	},
	
	_VK_get_user: function(vk_user_id) {
		js_vk_user_id = Pointer_stringify(vk_user_id);
		JS_VK_get_user(js_vk_user_id);
	},

	_VK_call_method: function( method ) {
		var str = Pointer_stringify(method);
		JS_VK_call_method( str ); 
	},

	_VK_postToWall: function( txt ) {
		var str = Pointer_stringify(txt);
		JS_VK_postToWall( str ); 
	}


});