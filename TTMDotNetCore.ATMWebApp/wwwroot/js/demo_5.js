"use strict"

var themeOptionArr = {
			typography: '',
			version: '',
			layout: '',
			primary: '',
			headerBg: '',
			navheaderBg: '',
			sidebarBg: '',
			sidebarStyle: '',
			sidebarPosition: '',
			headerPosition: '',
			containerLayout: '',
			direction: '',
		};



(function($) {
	
	"use strict"
	
	var direction =  getUrlParams('dir');
	var theme =  getUrlParams('theme');
	
	/* Dz Theme Demo Settings  */
	
	var dezThemeSet0 = { /* Default Theme */
		typography: "poppins",
		version: "light",
		layout: "vertical",
		headerBg: "color_1",
		primary: "color_1",
		navheaderBg: "color_1",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: direction
	};
	
	var dezThemeSet1 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_2",
		headerBg: "color_1",
		navheaderBg: "color_2",
		sidebarBg: "color_2",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dezThemeSet2 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_13",
		navheaderBg: "color_13",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
    
    var dezThemeSet3 = {
		typography: "poppins",
		version: "light",
		layout: "horizontal",
		primary: "color_14",
		headerBg: "color_1",
		navheaderBg: "color_1",
		sidebarBg: "color_14",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dezThemeSet4 = {
		typography: "poppins",
		version: "light",
		layout: "horizontal",
		primary: "color_6",
		headerBg: "color_6",
		navheaderBg: "color_6",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dezThemeSet5 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_4",
		headerBg: "color_1",
		navheaderBg: "color_4",
		sidebarBg: "color_4",
		sidebarStyle: "compact",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dezThemeSet6 = {
		typography: "poppins",
		version: "dark",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_1",
		navheaderBg: "color_1",
		sidebarBg: "color_1",
		sidebarStyle: "compact",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
    
    var dezThemeSet7 = {
		typography: "poppins",
		version: "dark",
		layout: "vertical",
		primary: "color_3",
		headerBg: "color_14",
		navheaderBg: "color_3",
		sidebarBg: "color_3",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
    
    var dezThemeSet8 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_1",
		navheaderBg: "color_3",
		sidebarBg: "color_3",
		sidebarStyle: "mini",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
    
	
		
	function themeChange(theme, direction){
		var themeSettings = {};
		themeSettings = eval('dezThemeSet'+theme);
		themeSettings.direction = direction;
		dezSettingsOptions = themeSettings; /* For Screen Resize */
		new dezSettings(themeSettings);
		
		setThemeInCookie(themeSettings);
	}
	
	function setThemeInCookie(themeSettings)
	{
		//console.log(themeSettings);
		jQuery.each(themeSettings, function(optionKey, optionValue) {
			setCookie(optionKey,optionValue);
		});
	}
	
	function setThemeLogo() {
		var logo = getCookie('logo_src');
		
		var logo2 = getCookie('logo_src2');
		
		if(logo != ''){
			jQuery('.nav-header .logo-abbr').attr("src", logo);
		}
		
		if(logo2 != ''){
			jQuery('.nav-header .logo-compact, .nav-header .brand-title').attr("src", logo2);
		}
	}

	/*  set switcher option start  */
	function getElementAttrs(el) {
		return [].slice.call(el.attributes).map((attr) => {
			return {
				name: attr.name,
				value: attr.value
			}
		});
	}
	
	function handleSetThemeOption(item, index, arr) {
		
		var attrName = item.name.replace('data-','').replace('-','_');
		
		if(attrName === "sidebarbg" || attrName === "primary" || attrName === "headerbg" || attrName === "nav_headerbg" ){
			if(item.value === "color_1"){
				return false;
			}
			var attrNameColor = attrName.replace("bg","")
			document.getElementById(attrNameColor+"_"+item.value).checked = true;
		}else if(attrName === "navigationbarimg"){
			document.getElementById("sidebar_img_"+item.value.split('sidebar-img/')[1].split('.')[0]).checked = true;
		}else if(attrName === "sidebartext"){
			document.getElementById("sidebar_text_"+item.value).checked = true;
		}else if(attrName === "direction" || attrName === "nav_headerbg" || attrName === "headerbg"){
			document.getElementById("theme_direction").value = item.value;	
		}else if(attrName === "sidebar_style" || attrName === "sidebar_position" || attrName === "header_position" || attrName === "typography" || attrName === "theme_version" ){
			if(item.value === "cairo" || item.value === "full" || item.value === "fixed"|| item.value === "light"){return false}
			document.getElementById(attrName).value = item.value;				
		}else if(attrName === "layout"){
			if(item.value === "vertical"){return false}
			document.getElementById("theme_layout").value = item.value;		
		}
		else if(attrName === "container"){
			if(item.value === "wide"){return false}
			document.getElementById("container_layout").value = item.value;
		}
		
		$('.default-select').selectpicker('refresh');
	}
	/* / set switcher option end / */
	
	function setThemeOptionOnPage()
	{
		if(getCookie('version') != '')
		{
			jQuery.each(themeOptionArr, function(optionKey, optionValue) {
				var optionData = getCookie(optionKey);
				themeOptionArr[optionKey] = (optionData != '')?optionData:dezSettingsOptions[optionKey];
			});
			//console.log(themeOptionArr);
			dezSettingsOptions = themeOptionArr;
			new dezSettings(dezSettingsOptions);
			
			setThemeLogo();
		}
	}
	
	jQuery(document).on('click', '.dz_theme_demo', function(){
		setTimeout(() => {
			var allAttrs = getElementAttrs(document.querySelector('body'));
			allAttrs.forEach(handleSetThemeOption);
		},1500);

		var demoTheme = jQuery(this).data('theme');
		themeChange(demoTheme, 'ltr');
		$('.dz-demo-panel').removeClass('show');
		jQuery('.main-css').attr('href','css/style.css');
	});


	jQuery(document).on('click', '.dz_theme_demo_rtl', function(){
		var demoTheme = jQuery(this).data('theme');
		themeChange(demoTheme, 'rtl');
		$('.dz-demo-panel').removeClass('show');
		jQuery('.main-css').attr('href','css/style-rtl.css');
	});
	
	
	jQuery(window).on('load', function(){
		direction = (direction != undefined) ? direction : 'ltr';

		if(getCookie('direction') == 'rtl'){
			jQuery('.main-css').attr('href','css/style-rtl.css');
		}

		if(theme != undefined){
			if(theme == 'rtl'){
				themeChange(0, 'rtl');
				jQuery('.main-css').attr('href','css/style-rtl.css');
			}else {
				themeChange(theme, direction);
			}
		}
		else if(direction != undefined){
			if(getCookie('version') == ''){	
				themeChange(0, direction);
			}
		}
		
		setTimeout(() => {
			var allAttrs = getElementAttrs(document.querySelector('body'));
			allAttrs.forEach(handleSetThemeOption);
		},1500);

		/* Set Theme On Page From Cookie */
		setThemeOptionOnPage();
	});
	jQuery(window).on('resize', function(){
		setThemeOptionOnPage();
	});
	

})(jQuery);