/**
* asColorPicker v0.4.4
* https://github.com/amazingSurge/jquery-asColorPicker
*
* Copyright (c) amazingSurge
* Released under the LGPL-3.0 license
*/
!function(t,e){if("function"==typeof define&&define.amd)define(["jquery","jquery-asColor","jquery-asGradient"],e);else if("undefined"!=typeof exports)e(require("jquery"),require("jquery-asColor"),require("jquery-asGradient"));else{var i={exports:{}};e(t.jQuery,t.AsColor,t.AsGradient),t.jqueryAsColorPickerEs=i.exports}}(this,function(t,e,i){"use strict";function a(t){return t&&t.__esModule?t:{default:t}}function n(t,e){if(!(t instanceof e))throw new TypeError("Cannot call a class as a function")}function s(t){return t<0?t=0:t>1&&(t=1),100*t+"%"}function o(t){t.id=P,P++}var r=a(t),h=a(e),l=a(i),u=function(){function t(t,e){for(var i=0;i<e.length;i++){var a=e[i];a.enumerable=a.enumerable||!1,a.configurable=!0,"value"in a&&(a.writable=!0),Object.defineProperty(t,a.key,a)}}return function(e,i,a){return i&&t(e.prototype,i),a&&t(e,a),e}}(),c={namespace:"asColorPicker",readonly:!1,skin:null,lang:"en",hideInput:!1,hideFireChange:!0,keyboard:!1,color:{format:!1,alphaConvert:{RGB:"RGBA",HSL:"HSLA",HEX:"RGBA",NAMESPACE:"RGBA"},shortenHex:!1,hexUseName:!1,reduceAlpha:!0,nameDegradation:"HEX",invalidValue:"",zeroAlphaAsTransparent:!0},mode:"simple",onInit:null,onReady:null,onChange:null,onClose:null,onOpen:null,onApply:null},d={simple:{trigger:!0,clear:!0,saturation:!0,hue:!0,alpha:!0},palettes:{trigger:!0,clear:!0,palettes:!0},complex:{trigger:!0,clear:!0,preview:!0,palettes:!0,saturation:!0,hue:!0,alpha:!0,hex:!0,buttons:!0},gradient:{trigger:!0,clear:!0,preview:!0,palettes:!0,saturation:!0,hue:!0,alpha:!0,hex:!0,gradient:!0}},p={size:150,defaults:{direction:"vertical",template:function(t){return'<div class="'+t+"-alpha "+t+"-alpha-"+this.direction+'"><i></i></div>'}},data:{},init:function(t,e){var i=this;this.options=$.extend(this.defaults,e),i.direction=this.options.direction,this.api=t,this.$alpha=$(this.options.template.call(i,t.namespace)).appendTo(t.$dropdown),this.$handle=this.$alpha.find("i"),t.$element.on("asColorPicker::firstOpen",function(){"vertical"===i.direction?i.size=i.$alpha.height():i.size=i.$alpha.width(),i.step=i.size/360,i.bindEvents(),i.keyboard()}),t.$element.on("asColorPicker::update asColorPicker::setup",function(t,e,a){i.update(a)})},bindEvents:function(){var t=this;this.$alpha.on(this.api.eventName("mousedown"),function(e){if(e.which?3===e.which:2===e.button)return!1;$.proxy(t.mousedown,t)(e)})},mousedown:function(t){var e=this.$alpha.offset();return"vertical"===this.direction?(this.data.startY=t.pageY,this.data.top=t.pageY-e.top,this.move(this.data.top)):(this.data.startX=t.pageX,this.data.left=t.pageX-e.left,this.move(this.data.left)),this.mousemove=function(t){var e=void 0;return e="vertical"===this.direction?this.data.top+(t.pageY||this.data.startY)-this.data.startY:this.data.left+(t.pageX||this.data.startX)-this.data.startX,this.move(e),!1},this.mouseup=function(){return $(document).off({mousemove:this.mousemove,mouseup:this.mouseup}),"vertical"===this.direction?this.data.top=this.data.cach:this.data.left=this.data.cach,!1},$(document).on({mousemove:$.proxy(this.mousemove,this),mouseup:$.proxy(this.mouseup,this)}),!1},move:function(t,e,i){t=Math.max(0,Math.min(this.size,t)),this.data.cach=t,void 0===e&&(e=1-t/this.size),e=Math.max(0,Math.min(1,e)),"vertical"===this.direction?this.$handle.css({top:t}):this.$handle.css({left:t}),!1!==i&&this.api.set({a:Math.round(100*e)/100})},moveLeft:function(){var t=this.step,e=this.data;e.left=Math.max(0,Math.min(this.width,e.left-t)),this.move(e.left)},moveRight:function(){var t=this.step,e=this.data;e.left=Math.max(0,Math.min(this.width,e.left+t)),this.move(e.left)},moveUp:function(){var t=this.step,e=this.data;e.top=Math.max(0,Math.min(this.width,e.top-t)),this.move(e.top)},moveDown:function(){var t=this.step,e=this.data;e.top=Math.max(0,Math.min(this.width,e.top+t)),this.move(e.top)},keyboard:function(){var t=void 0,e=this;if(!this.api._keyboard)return!1;t=$.extend(!0,{},this.api._keyboard),this.$alpha.attr("tabindex","0").on("focus",function(){return"vertical"===this.direction?t.attach({up:function(){e.moveUp()},down:function(){e.moveDown()}}):t.attach({left:function(){e.moveLeft()},right:function(){e.moveRight()}}),!1}).on("blur",function(){t.detach()})},update:function(t){var e=this.size*(1-t.value.a);this.$alpha.css("backgroundColor",t.toHEX()),this.move(e,t.value.a,!1)},destroy:function(){$(document).off({mousemove:this.mousemove,mouseup:this.mouseup})}},f={init:function(t){var e='<input type="text" class="'+t.namespace+'-hex" />';this.$hex=$(e).appendTo(t.$dropdown),this.$hex.on("change",function(){t.set(this.value)});var i=this;t.$element.on("asColorPicker::update asColorPicker::setup",function(t,e,a){i.update(a)})},update:function(t){this.$hex.val(t.toHEX())}},v={size:150,defaults:{direction:"vertical",template:function(){var t=this.api.namespace;return'<div class="'+t+"-hue "+t+"-hue-"+this.direction+'"><i></i></div>'}},data:{},init:function(t,e){var i=this;this.options=$.extend(this.defaults,e),this.direction=this.options.direction,this.api=t,this.$hue=$(this.options.template.call(i)).appendTo(t.$dropdown),this.$handle=this.$hue.find("i"),t.$element.on("asColorPicker::firstOpen",function(){"vertical"===i.direction?i.size=i.$hue.height():i.size=i.$hue.width(),i.step=i.size/360,i.bindEvents(t),i.keyboard(t)}),t.$element.on("asColorPicker::update asColorPicker::setup",function(t,e,a){i.update(a)})},bindEvents:function(){var t=this;this.$hue.on(this.api.eventName("mousedown"),function(e){if(e.which?3===e.which:2===e.button)return!1;$.proxy(t.mousedown,t)(e)})},mousedown:function(t){var e=this.$hue.offset();return"vertical"===this.direction?(this.data.startY=t.pageY,this.data.top=t.pageY-e.top,this.move(this.data.top)):(this.data.startX=t.pageX,this.data.left=t.pageX-e.left,this.move(this.data.left)),this.mousemove=function(t){var e=void 0;return e="vertical"===this.direction?this.data.top+(t.pageY||this.data.startY)-this.data.startY:this.data.left+(t.pageX||this.data.startX)-this.data.startX,this.move(e),!1},this.mouseup=function(){return $(document).off({mousemove:this.mousemove,mouseup:this.mouseup}),"vertical"===this.direction?this.data.top=this.data.cach:this.data.left=this.data.cach,!1},$(document).on({mousemove:$.proxy(this.mousemove,this),mouseup:$.proxy(this.mouseup,this)}),!1},move:function(t,e,i){t=Math.max(0,Math.min(this.size,t)),this.data.cach=t,void 0===e&&(e=360*(1-t/this.size)),e=Math.max(0,Math.min(360,e)),"vertical"===this.direction?this.$handle.css({top:t}):this.$handle.css({left:t}),!1!==i&&this.api.set({h:e})},moveLeft:function(){var t=this.step,e=this.data;e.left=Math.max(0,Math.min(this.width,e.left-t)),this.move(e.left)},moveRight:function(){var t=this.step,e=this.data;e.left=Math.max(0,Math.min(this.width,e.left+t)),this.move(e.left)},moveUp:function(){var t=this.step,e=this.data;e.top=Math.max(0,Math.min(this.width,e.top-t)),this.move(e.top)},moveDown:function(){var t=this.step,e=this.data;e.top=Math.max(0,Math.min(this.width,e.top+t)),this.move(e.top)},keyboard:function(){var t=void 0,e=this;if(!this.api._keyboard)return!1;t=$.extend(!0,{},this.api._keyboard),this.$hue.attr("tabindex","0").on("focus",function(){return"vertical"===this.direction?t.attach({up:function(){e.moveUp()},down:function(){e.moveDown()}}):t.attach({left:function(){e.moveLeft()},right:function(){e.moveRight()}}),!1}).on("blur",function(){t.detach()})},update:function(t){var e=0===t.value.h?0:this.size*(1-t.value.h/360);this.move(e,t.value.h,!1)},destroy:function(){$(document).off({mousemove:this.mousemove,mouseup:this.mouseup})}},m={defaults:{template:function(t){return'<div class="'+t+'-saturation"><i><b></b></i></div>'}},width:0,height:0,size:6,data:{},init:function(t,e){var i=this;this.options=$.extend(this.defaults,e),this.api=t,this.$saturation=$(this.options.template.call(i,t.namespace)).appendTo(t.$dropdown),this.$handle=this.$saturation.find("i"),t.$element.on("asColorPicker::firstOpen",function(){i.width=i.$saturation.width(),i.height=i.$saturation.height(),i.step={left:i.width/20,top:i.height/20},i.size=i.$handle.width()/2,i.bindEvents(),i.keyboard(t)}),t.$element.on("asColorPicker::update asColorPicker::setup",function(t,e,a){i.update(a)})},bindEvents:function(){var t=this;this.$saturation.on(this.api.eventName("mousedown"),function(e){if(e.which?3===e.which:2===e.button)return!1;t.mousedown(e)})},mousedown:function(t){var e=this.$saturation.offset();return this.data.startY=t.pageY,this.data.startX=t.pageX,this.data.top=t.pageY-e.top,this.data.left=t.pageX-e.left,this.data.cach={},this.move(this.data.left,this.data.top),this.mousemove=function(t){var e=this.data.left+(t.pageX||this.data.startX)-this.data.startX,i=this.data.top+(t.pageY||this.data.startY)-this.data.startY;return this.move(e,i),!1},this.mouseup=function(){return $(document).off({mousemove:this.mousemove,mouseup:this.mouseup}),this.data.left=this.data.cach.left,this.data.top=this.data.cach.top,!1},$(document).on({mousemove:$.proxy(this.mousemove,this),mouseup:$.proxy(this.mouseup,this)}),!1},move:function(t,e,i){e=Math.max(0,Math.min(this.height,e)),t=Math.max(0,Math.min(this.width,t)),void 0===this.data.cach&&(this.data.cach={}),this.data.cach.left=t,this.data.cach.top=e,this.$handle.css({top:e-this.size,left:t-this.size}),!1!==i&&this.api.set({s:t/this.width,v:1-e/this.height})},update:function(t){void 0===t.value.h&&(t.value.h=0),this.$saturation.css("backgroundColor",h.default.HSLtoHEX({h:t.value.h,s:1,l:.5}));var e=t.value.s*this.width,i=(1-t.value.v)*this.height;this.move(e,i,!1)},moveLeft:function(){var t=this.step.left,e=this.data;e.left=Math.max(0,Math.min(this.width,e.left-t)),this.move(e.left,e.top)},moveRight:function(){var t=this.step.left,e=this.data;e.left=Math.max(0,Math.min(this.width,e.left+t)),this.move(e.left,e.top)},moveUp:function(){var t=this.step.top,e=this.data;e.top=Math.max(0,Math.min(this.width,e.top-t)),this.move(e.left,e.top)},moveDown:function(){var t=this.step.top,e=this.data;e.top=Math.max(0,Math.min(this.width,e.top+t)),this.move(e.left,e.top)},keyboard:function(){var t=void 0,e=this;if(!this.api._keyboard)return!1;t=$.extend(!0,{},this.api._keyboard),this.$saturation.attr("tabindex","0").on("focus",function(){return t.attach({left:function(){e.moveLeft()},right:function(){e.moveRight()},up:function(){e.moveUp()},down:function(){e.moveDown()}}),!1}).on("blur",function(){t.detach()})},destroy:function(){$(document).off({mousemove:this.mousemove,mouseup:this.mouseup})}},g={defaults:{apply:!1,cancel:!0,applyText:null,cancelText:null,template:function(t){return'<div class="'+t+'-buttons"></div>'},applyTemplate:function(t){return'<a href="#" alt="'+this.options.applyText+'" class="'+t+'-buttons-apply">'+this.options.applyText+"</a>"},cancelTemplate:function(t){return'<a href="#" alt="'+this.options.cancelText+'" class="'+t+'-buttons-apply">'+this.options.cancelText+"</a>"}},init:function(t,e){var i=this;this.options=$.extend(this.defaults,{applyText:t.getString("applyText","apply"),cancelText:t.getString("cancelText","cancel")},e),this.$buttons=$(this.options.template.call(this,t.namespace)).appendTo(t.$dropdown),t.$element.on("asColorPicker::firstOpen",function(){i.options.apply&&(i.$apply=$(i.options.applyTemplate.call(i,t.namespace)).appendTo(i.$buttons).on("click",function(){return t.apply(),!1})),i.options.cancel&&(i.$cancel=$(i.options.cancelTemplate.call(i,t.namespace)).appendTo(i.$buttons).on("click",function(){return t.cancel(),!1}))})}},y={defaults:{template:function(t){return'<div class="'+t+'-trigger"><span></span></div>'}},init:function(t,e){this.options=$.extend(this.defaults,e),t.$trigger=$(this.options.template.call(this,t.namespace)),this.$triggerInner=t.$trigger.children("span"),t.$trigger.insertAfter(t.$element),t.$trigger.on("click",function(){return t.opened?t.close():t.open(),!1});var i=this;t.$element.on("asColorPicker::update",function(t,e,a,n){void 0===n&&(n=!1),i.update(a,n)}),this.update(t.color)},update:function(t,e){e?this.$triggerInner.css("background",e.toString(!0)):this.$triggerInner.css("background",t.toRGBA())},destroy:function(t){t.$trigger.remove()}},w={defaults:{template:function(t){return'<a href="#" class="'+t+'-clear"></a>'}},init:function(t,e){t.options.hideInput||(this.options=$.extend(this.defaults,e),this.$clear=$(this.options.template.call(this,t.namespace)).insertAfter(t.$element),this.$clear.on("click",function(){return t.clear(),!1}))}},k={color:["white","black","transparent"],init:function(t){var e='<ul class="'+t.namespace+'-info"><li><label>R:<input type="text" data-type="r"/></label></li><li><label>G:<input type="text" data-type="g"/></label></li><li><label>B:<input type="text" data-type="b"/></label></li><li><label>A:<input type="text" data-type="a"/></label></li></ul>';this.$info=$(e).appendTo(t.$dropdown),this.$r=this.$info.find('[data-type="r"]'),this.$g=this.$info.find('[data-type="g"]'),this.$b=this.$info.find('[data-type="b"]'),this.$a=this.$info.find('[data-type="a"]'),this.$info.on(t.eventName("keyup update change"),"input",function(e){var i=void 0,a=$(e.target).data("type");switch(a){case"r":case"g":case"b":(i=parseInt(this.value,10))>255?i=255:i<0&&(i=0);break;case"a":(i=parseFloat(this.value,10))>1?i=1:i<0&&(i=0)}isNaN(i)&&(i=0);var n={};n[a]=i,t.set(n)});var i=this;t.$element.on("asColorPicker::update asColorPicker::setup",function(t,e){i.update(e)})},update:function(t){this.$r.val(t.value.r),this.$g.val(t.value.g),this.$b.val(t.value.b),this.$a.val(t.value.a)}};window.localStorage||(window.localStorage=function(){});var b={defaults:{template:function(t){return'<ul class="'+t+'-palettes"></ul>'},item:function(t,e){return'<li data-color="'+e+'"><span style="background-color:'+e+'" /></li>'},colors:["white","black","red","blue","yellow"],max:10,localStorage:!0},init:function(t,e){var i=this,a=void 0,n=(0,h.default)();this.options=$.extend(!0,{},this.defaults,e),this.colors=[];var s=void 0;this.options.localStorage?(s=t.namespace+"_palettes_"+t.id,(a=this.getLocal(s))||(a=this.options.colors,this.setLocal(s,a))):a=this.options.colors;for(var o in a)Object.hasOwnProperty.call(a,o)&&this.colors.push(n.val(a[o]).toRGBA());var r="";$.each(this.colors,function(e,a){r+=i.options.item(t.namespace,a)}),this.$palettes=$(this.options.template.call(this,t.namespace)).html(r).appendTo(t.$dropdown),this.$palettes.on(t.eventName("click"),"li",function(e){var i=$(this).data("color");t.set(i),e.preventDefault(),e.stopPropagation()}),t.$element.on("asColorPicker::apply",function(t,e,a){"function"!=typeof a.toRGBA&&(a=a.get().color);var n=a.toRGBA();-1===$.inArray(n,i.colors)&&(i.colors.length>=i.options.max&&(i.colors.shift(),i.$palettes.find("li").eq(0).remove()),i.colors.push(n),i.$palettes.append(i.options.item(e.namespace,a)),i.options.localStorage&&i.setLocal(s,i.colors))})},setLocal:function(t,e){var i=JSON.stringify(e);localStorage[t]=i},getLocal:function(t){var e=localStorage[t];return e?JSON.parse(e):e}},x={defaults:{template:function(t){return'<ul class="'+t+'-preview"><li class="'+t+'-preview-current"><span /></li><li class="'+t+'-preview-previous"><span /></li></ul>'}},init:function(t,e){var i=this;this.options=$.extend(this.defaults,e),this.$preview=$(this.options.template.call(i,t.namespace)).appendTo(t.$dropdown),this.$current=this.$preview.find("."+t.namespace+"-preview-current span"),this.$previous=this.$preview.find("."+t.namespace+"-preview-previous span"),t.$element.on("asColorPicker::firstOpen",function(){i.$previous.on("click",function(){return t.set($(this).data("color")),!1})}),t.$element.on("asColorPicker::setup",function(t,e,a){i.updateCurrent(a),i.updatePreview(a)}),t.$element.on("asColorPicker::update",function(t,e,a){i.updateCurrent(a)})},updateCurrent:function(t){this.$current.css("backgroundColor",t.toRGBA())},updatePreview:function(t){this.$previous.css("backgroundColor",t.toRGBA()),this.$previous.data("color",{r:t.value.r,g:t.value.g,b:t.value.b,a:t.value.a})}},C=function(t,e){this.api=t,this.options=e,this.classes={enable:t.namespace+"-gradient_enable",marker:t.namespace+"-gradient-marker",active:t.namespace+"-gradient-marker_active",focus:t.namespace+"-gradient_focus"},this.isEnabled=!1,this.initialized=!1,this.current=null,this.value=(0,l.default)(this.options.settings),this.$doc=$(document);var i=this;$.extend(i,{init:function(){i.$wrap=$(i.options.template.call(i)).appendTo(t.$dropdown),i.$gradient=i.$wrap.filter("."+t.namespace+"-gradient"),this.angle.init(),this.preview.init(),this.markers.init(),this.wheel.init(),this.bind(),(!1===i.options.switchable||this.value.matchString(t.element.value))&&i.enable(),this.initialized=!0},bind:function(){var e=t.namespace;i.$gradient.on("update",function(){var e=i.value.getById(i.current);e&&t._trigger("update",e.color,i.value),t.element.value!==i.value.toString()&&t._updateInput()}),i.options.switchable&&i.$wrap.on("click","."+e+"-gradient-switch",function(){return i.isEnabled?i.disable():i.enable(),!1}),i.$wrap.on("click","."+e+"-gradient-cancel",function(){return(!1===i.options.switchable||l.default.matchString(t.originValue))&&i.overrideCore(),t.cancel(),!1})},overrideCore:function(){t.set=function(e){if(t.isEmpty=""===e,"string"==typeof e)!1===i.options.switchable||l.default.matchString(e)?i.isEnabled?(i.val(e),t.color=i.value,i.$gradient.trigger("update",i.value.value)):i.enable(e):(i.disable(),t.val(e));else{var a=i.value.getById(i.current);a&&(a.color.val(e),t._trigger("update",a.color,i.value)),i.$gradient.trigger("update",{id:i.current,stop:a})}},t._setup=function(){var e=i.value.getById(i.current);t._trigger("setup",e.color)}},revertCore:function(){t.set=$.proxy(t._set,t),t._setup=function(){t._trigger("setup",t.color)}},preview:{init:function(){var e=this;i.$preview=i.$gradient.find("."+t.namespace+"-gradient-preview"),i.$gradient.on("add del update empty",function(){e.render()})},render:function(){i.$preview.css({"background-image":i.value.toStringWithAngle("to right",!0)}),i.$preview.css({"background-image":i.value.toStringWithAngle("to right")})}},markers:{width:160,init:function(){var e=this;i.$markers=i.$gradient.find("."+t.namespace+"-gradient-markers").attr("tabindex",0),i.$gradient.on("add",function(t,i){e.add(i.stop)}),i.$gradient.on("active",function(t,i){e.active(i.id)}),i.$gradient.on("del",function(t,i){e.del(i.id)}),i.$gradient.on("update",function(t,i){i.stop&&e.update(i.stop.id,i.stop.color)}),i.$gradient.on("empty",function(){e.empty()}),i.$markers.on(i.api.eventName("mousedown"),function(t){if(t.which?3===t.which:2===t.button)return!1;var e=parseFloat((t.pageX-i.$markers.offset().left)/i.markers.width,10);return i.add("#fff",e),!1});var a=this;i.$markers.on(i.api.eventName("mousedown"),"li",function(t){return(t.which?3!==t.which:2!==t.button)&&(a.mousedown(this,t),!1)}),i.$doc.on(i.api.eventName("keydown"),function(t){if(i.api.opened&&i.$markers.is("."+i.classes.focus)){var e=t.keyCode||t.which;if(46===e||8===e)return!(i.value.length<=2)&&(i.del(i.current),!1)}}),i.$markers.on(i.api.eventName("focus"),function(){i.$markers.addClass(i.classes.focus)}).on(i.api.eventName("blur"),function(){i.$markers.removeClass(i.classes.focus)}),i.$markers.on(i.api.eventName("click"),"li",function(){var t=$(this).data("id");i.active(t)})},getMarker:function(t){return i.$markers.find('[data-id="'+t+'"]')},update:function(t,e){var i=this.getMarker(t);i.find("span").css("background-color",e.toHEX()),i.find("i").css("background-color",e.toHEX())},add:function(t){$('<li data-id="'+t.id+'" style="left:'+s(t.position)+'" class="'+i.classes.marker+'"><span style="background-color: '+t.color.toHEX()+'"></span><i style="background-color: '+t.color.toHEX()+'"></i></li>').appendTo(i.$markers)},empty:function(){i.$markers.html("")},del:function(t){var e=this.getMarker(t),a=e.prev();0===a.length&&(a=e.next()),i.active(a.data("id")),e.remove()},active:function(t){i.$markers.children().removeClass(i.classes.active),this.getMarker(t).addClass(i.classes.active),i.$markers.focus()},mousedown:function(t,e){var a=this,n=$(t).data("id"),s=$(t).position().left,o=e.pageX,r=void 0;return this.mousemove=function(e){r=e.pageX||o;var i=(s+r-o)/this.width;return a.move(t,i,n),!1},this.mouseup=function(){return $(document).off({mousemove:this.mousemove,mouseup:this.mouseup}),!1},i.$doc.on({mousemove:$.proxy(this.mousemove,this),mouseup:$.proxy(this.mouseup,this)}),i.active(n),!1},move:function(t,e,a){i.api.isEmpty=!1,e=Math.max(0,Math.min(1,e)),$(t).css({left:s(e)}),a||(a=$(t).data("id")),i.value.getById(a).setPosition(e),i.$gradient.trigger("update",{id:$(t).data("id"),position:e})}},wheel:{init:function(){var e=this;i.$wheel=i.$gradient.find("."+t.namespace+"-gradient-wheel"),i.$pointer=i.$wheel.find("i"),i.$gradient.on("update",function(t,i){void 0!==i.angle&&e.position(i.angle)}),i.$wheel.on(i.api.eventName("mousedown"),function(t){return(t.which?3!==t.which:2!==t.button)&&(e.mousedown(t,i),!1)})},mousedown:function(t,e){var i=this,a=e.$wheel.offset(),n=e.$wheel.width()/2,s=a.left+n,o=a.top+n,r=e.$doc;this.r=n,this.wheelMove=function(t){var a=t.pageX-s,n=o-t.pageY,r=i.getPosition(a,n),h=i.calAngle(r.x,r.y);e.api.isEmpty=!1,e.setAngle(h)},this.wheelMouseup=function(){return r.off({mousemove:this.wheelMove,mouseup:this.wheelMouseup}),!1},r.on({mousemove:$.proxy(this.wheelMove,this),mouseup:$.proxy(this.wheelMouseup,this)}),this.wheelMove(t)},getPosition:function(t,e){var i=this.r;return{x:t/Math.sqrt(t*t+e*e)*i,y:e/Math.sqrt(t*t+e*e)*i}},calAngle:function(t,e){var i=Math.round(Math.atan(Math.abs(t/e))*(180/Math.PI));return t<0&&e>0?360-i:t<0&&e<=0?i+180:t>=0&&e<=0?180-i:t>=0&&e>0?i:void 0},set:function(t){i.value.angle(t),i.$gradient.trigger("update",{angle:t})},position:function(t){var e=this.r||i.$wheel.width()/2,a=this.calPointer(t,e);i.$pointer.css({left:a.x,top:a.y})},calPointer:function(t,e){return{x:e+Math.sin(t*Math.PI/180)*e,y:e-Math.cos(t*Math.PI/180)*e}}},angle:{init:function(){i.$angle=i.$gradient.find("."+t.namespace+"-gradient-angle"),i.$angle.on(i.api.eventName("blur"),function(){return i.setAngle(this.value),!1}).on(i.api.eventName("keydown"),function(t){if(13===(t.keyCode||t.which))return i.api.isEmpty=!1,$(this).blur(),!1}),i.$gradient.on("update",function(t,e){void 0!==e.angle&&i.$angle.val(e.angle)})},set:function(t){i.value.angle(t),i.$gradient.trigger("update",{angle:t})}}}),this.init()};C.prototype={constructor:C,enable:function(t){!0!==this.isEnabled&&(this.isEnabled=!0,this.overrideCore(),this.$gradient.addClass(this.classes.enable),this.markers.width=this.$markers.width(),void 0===t&&(t=this.api.element.value),this.api.isEmpty=""===t,!l.default.matchString(t)&&this._last?this.value=this._last:this.val(t),this.api.color=this.value,this.$gradient.trigger("update",this.value.value),this.api.opened&&this.api.position())},val:function(t){if(""===t||this.value.toString()!==t){if(this.empty(),this.value.val(t),this.value.reorder(),this.value.length<2){var e=t;h.default.matchString(t)||(e="rgba(0,0,0,1)"),0===this.value.length&&this.value.append(e,0),1===this.value.length&&this.value.append(e,1)}for(var i=void 0,a=0;a<this.value.length;a++)(i=this.value.get(a))&&this.$gradient.trigger("add",{stop:i});this.active(i.id)}},disable:function(){!1!==this.isEnabled&&(this.isEnabled=!1,this.revertCore(),this.$gradient.removeClass(this.classes.enable),this._last=this.value,this.api.color=this.api.color.getCurrent().color,this.api.set(this.api.color.value),this.api.opened&&this.api.position())},active:function(t){this.current!==t&&(this.current=t,this.value.setCurrentById(t),this.$gradient.trigger("active",{id:t}))},empty:function(){this.value.empty(),this.$gradient.trigger("empty")},add:function(t,e){var i=this.value.insert(t,e);return this.api.isEmpty=!1,this.value.reorder(),this.$gradient.trigger("add",{stop:i}),this.active(i.id),this.$gradient.trigger("update",{stop:i}),i},del:function(t){this.value.length<=2||(this.value.removeById(t),this.value.reorder(),this.$gradient.trigger("del",{id:t}),this.$gradient.trigger("update",{}))},setAngle:function(t){this.value.angle(t),this.$gradient.trigger("update",{angle:t})}};var M={defaults:{switchable:!0,switchText:"Gradient",cancelText:"Cancel",settings:{forceStandard:!0,angleUseKeyword:!0,emptyString:"",degradationFormat:!1,cleanPosition:!1,color:{format:"rgb"}},template:function(){var t=this.api.namespace,e='<div class="'+t+'-gradient-control">';return this.options.switchable&&(e+='<a href="#" class="'+t+'-gradient-switch">'+this.options.switchText+"</a>"),(e+='<a href="#" class="'+t+'-gradient-cancel">'+this.options.cancelText+"</a></div>")+'<div class="'+t+'-gradient"><div class="'+t+'-gradient-preview"><ul class="'+t+'-gradient-markers"></ul></div><div class="'+t+'-gradient-wheel"><i></i></div><input class="'+t+'-gradient-angle" type="text" value="" size="3" /></div>'}},init:function(t,e){var i=this;t.$element.on("asColorPicker::ready",function(a,n){"gradient"===n.options.mode&&(i.defaults.settings.color=t.options.color,e=$.extend(!0,i.defaults,e),t.gradient=new C(t,e))})}},T={},_={en:{cancelText:"cancel",applyText:"apply"}},P=0,z=function(){function t(e,i){n(this,t),this.element=e,this.$element=(0,r.default)(e),this.opened=!1,this.firstOpen=!0,this.disabled=!1,this.initialed=!1,this.originValue=this.element.value,this.isEmpty=!1,o(this),this.options=r.default.extend(!0,{},c,i,this.$element.data()),this.namespace=this.options.namespace,this.classes={wrap:this.namespace+"-wrap",dropdown:this.namespace+"-dropdown",input:this.namespace+"-input",skin:this.namespace+"_"+this.options.skin,open:this.namespace+"_open",mask:this.namespace+"-mask",hideInput:this.namespace+"_hideInput",disabled:this.namespace+"_disabled",mode:this.namespace+"-mode_"+this.options.mode},this.options.hideInput&&this.$element.addClass(this.classes.hideInput),this.components=d[this.options.mode],this._components=r.default.extend(!0,{},T),this._trigger("init"),this.init()}return u(t,[{key:"_trigger",value:function(t){for(var e=arguments.length,i=Array(e>1?e-1:0),a=1;a<e;a++)i[a-1]=arguments[a];var n=[this].concat(i);this.$element.trigger("asColorPicker::"+t,n);var s="on"+(t=t.replace(/\b\w+\b/g,function(t){return t.substring(0,1).toUpperCase()+t.substring(1)}));"function"==typeof this.options[s]&&this.options[s].apply(this,i)}},{key:"eventName",value:function(t){if("string"!=typeof t||""===t)return"."+this.options.namespace;for(var e=(t=t.split(" ")).length,i=0;i<e;i++)t[i]=t[i]+"."+this.options.namespace;return t.join(" ")}},{key:"init",value:function(){this.color=(0,h.default)(this.element.value,this.options.color),this._create(),this.options.skin&&(this.$dropdown.addClass(this.classes.skin),this.$element.parent().addClass(this.classes.skin)),this.options.readonly&&this.$element.prop("readonly",!0),this._bindEvent(),this.initialed=!0,this._trigger("ready")}},{key:"_create",value:function(){var t=this;this.$dropdown=(0,r.default)('<div class="'+this.classes.dropdown+'" data-mode="'+this.options.mode+'"></div>'),this.$element.wrap('<div class="'+this.classes.wrap+'"></div>').addClass(this.classes.input),this.$wrap=this.$element.parent(),this.$body=(0,r.default)("body"),this.$dropdown.data("asColorPicker",this);var e=void 0;r.default.each(this.components,function(i,a){!0===a&&(a={}),void 0!==t.options[i]&&(a=r.default.extend(!0,{},a,t.options[i])),Object.hasOwnProperty.call(t._components,i)&&(e=t._components[i]).init(t,a)}),this._trigger("create")}},{key:"_bindEvent",value:function(){var t=this;this.$element.on(this.eventName("click"),function(){return t.opened||t.open(),!1}),this.$element.on(this.eventName("keydown"),function(e){9===e.keyCode?t.close():13===e.keyCode&&(t.val(t.element.value),t.close())}),this.$element.on(this.eventName("keyup"),function(){t.color.matchString(t.element.value)&&t.val(t.element.value)})}},{key:"opacity",value:function(t){if(!t)return this.color.alpha();this.color.alpha(t)}},{key:"position",value:function(){var t=!this.$element.is(":visible"),e=t?this.$trigger.offset():this.$element.offset(),i=t?this.$trigger.outerHeight():this.$element.outerHeight(),a=t?this.$trigger.outerWidth():this.$element.outerWidth()+this.$trigger.outerWidth(),n=this.$dropdown.outerWidth(!0),s=this.$dropdown.outerHeight(!0),o=void 0,h=void 0;o=s+e.top>(0,r.default)(window).height()+(0,r.default)(window).scrollTop()?e.top-s:e.top+i,h=n+e.left>(0,r.default)(window).width()+(0,r.default)(window).scrollLeft()?e.left-n+a:e.left,this.$dropdown.css({position:"absolute",top:o,left:h})}},{key:"open",value:function(){this.disabled||(this.originValue=this.element.value,this.$dropdown[0]!==this.$body.children().last()[0]&&this.$dropdown.detach().appendTo(this.$body),this.$mask=(0,r.default)("."+this.classes.mask),0===this.$mask.length&&this.createMask(),this.$dropdown.prev()[0]!==this.$mask[0]&&this.$dropdown.before(this.$mask),(0,r.default)("#asColorPicker-dropdown").removeAttr("id"),this.$dropdown.attr("id","asColorPicker-dropdown"),this.$mask.show(),this.position(),(0,r.default)(window).on(this.eventName("resize"),r.default.proxy(this.position,this)),this.$dropdown.addClass(this.classes.open),this.opened=!0,this.firstOpen&&(this.firstOpen=!1,this._trigger("firstOpen")),this._setup(),this._trigger("open"))}},{key:"createMask",value:function(){this.$mask=(0,r.default)(document.createElement("div")),this.$mask.attr("class",this.classes.mask),this.$mask.hide(),this.$mask.appendTo(this.$body),this.$mask.on(this.eventName("mousedown touchstart click"),function(t){var e=(0,r.default)("#asColorPicker-dropdown"),i=void 0;e.length>0&&((i=e.data("asColorPicker")).opened&&(i.options.hideFireChange?i.apply():i.cancel()),t.preventDefault(),t.stopPropagation())})}},{key:"close",value:function(){this.opened=!1,this.$element.blur(),this.$mask.hide(),this.$dropdown.removeClass(this.classes.open),(0,r.default)(window).off(this.eventName("resize")),this._trigger("close")}},{key:"clear",value:function(){this.val("")}},{key:"cancel",value:function(){this.close(),this.set(this.originValue)}},{key:"apply",value:function(){this._trigger("apply",this.color),this.close()}},{key:"val",value:function(t){if(void 0===t)return this.color.toString();this.set(t)}},{key:"_update",value:function(){this._trigger("update",this.color),this._updateInput()}},{key:"_updateInput",value:function(){var t=this.color.toString();this.isEmpty&&(t=""),this._trigger("change",t),this.$element.val(t)}},{key:"set",value:function(t){return this.isEmpty=""===t,this._set(t)}},{key:"_set",value:function(t){"string"==typeof t?this.color.val(t):this.color.set(t),this._update()}},{key:"_setup",value:function(){this._trigger("setup",this.color)}},{key:"get",value:function(){return this.color}},{key:"enable",value:function(){return this.disabled=!1,this.$parent.addClass(this.classes.disabled),this._trigger("enable"),this}},{key:"disable",value:function(){return this.disabled=!0,this.$parent.removeClass(this.classes.disabled),this._trigger("disable"),this}},{key:"destroy",value:function(){return this.$element.unwrap(),this.$element.off(this.eventName()),this.$mask.remove(),this.$dropdown.remove(),this.initialized=!1,this.$element.data("asColorPicker",null),this._trigger("destroy"),this}},{key:"getString",value:function(t,e){return this.options.lang in _&&void 0!==_[this.options.lang][t]?_[this.options.lang][t]:e}}],[{key:"setLocalization",value:function(t,e){_[t]=e}},{key:"registerComponent",value:function(t,e){T[t]=e}},{key:"setDefaults",value:function(t){r.default.extend(!0,c,r.default.isPlainObject(t)&&t)}}]),t}();z.registerComponent("alpha",p),z.registerComponent("hex",f),z.registerComponent("hue",v),z.registerComponent("saturation",m),z.registerComponent("buttons",g),z.registerComponent("trigger",y),z.registerComponent("clear",w),z.registerComponent("info",k),z.registerComponent("palettes",b),z.registerComponent("preview",x),z.registerComponent("gradient",M),z.setLocalization("cn",{cancelText:"取消",applyText:"应用"}),z.setLocalization("de",{cancelText:"Abbrechen",applyText:"Wählen"}),z.setLocalization("dk",{cancelText:"annuller",applyText:"Vælg"}),z.setLocalization("es",{cancelText:"Cancelar",applyText:"Elegir"}),z.setLocalization("fi",{cancelText:"Kumoa",applyText:"Valitse"}),z.setLocalization("fr",{cancelText:"Annuler",applyText:"Valider"}),z.setLocalization("it",{cancelText:"annulla",applyText:"scegli"}),z.setLocalization("ja",{cancelText:"中止",applyText:"選択"}),z.setLocalization("ru",{cancelText:"отмена",applyText:"выбрать"}),z.setLocalization("sv",{cancelText:"Avbryt",applyText:"Välj"}),z.setLocalization("tr",{cancelText:"Avbryt",applyText:"Välj"});var E={version:"0.4.4"},A=r.default.fn.asColorPicker,S=function(t){for(var e=arguments.length,i=Array(e>1?e-1:0),a=1;a<e;a++)i[a-1]=arguments[a];if("string"==typeof t){var n=t;if(/^_/.test(n))return!1;if(!(/^(get)$/.test(n)||"val"===n&&0===i.length))return this.each(function(){var t=r.default.data(this,"asColorPicker");t&&"function"==typeof t[n]&&t[n].apply(t,i)});var s=this.first().data("asColorPicker");if(s&&"function"==typeof s[n])return s[n].apply(s,i)}return this.each(function(){(0,r.default)(this).data("asColorPicker")||(0,r.default)(this).data("asColorPicker",new z(this,t))})};r.default.fn.asColorPicker=S,r.default.asColorPicker=r.default.extend({setDefaults:z.setDefaults,registerComponent:z.registerComponent,setLocalization:z.setLocalization,noConflict:function(){return r.default.fn.asColorPicker=A,S}},E)});

