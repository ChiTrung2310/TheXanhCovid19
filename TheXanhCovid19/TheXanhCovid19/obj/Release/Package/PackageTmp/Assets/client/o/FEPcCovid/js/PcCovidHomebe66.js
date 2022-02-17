//alert(navigator.userAgent);

var parser = new UAParser();
var result = parser.getResult();
var userDevice = result.device.vendor;
function textAbstract(txt, maxlength) {
    if (txt.length <= maxlength) {
      return txt;
    }
    var t = txt.substring(0, maxlength);
    var re = /\s+\S*$/;
    var m = re.exec(t);
    t = t.substring(0, m.index);
    return t + "...";
}
function rentHomeNews(listNew){
	var jsonListNew = JSON.parse(listNew);
	var BzEntries = jsonListNew.BzEntry;
	var HasBzEntries = jsonListNew.HasBzEntry;
	var mainNewBody = "";
	var subNewBody = "";
	var count = 0;
	for(var hasbzentry in HasBzEntries){
		var number = HasBzEntries[hasbzentry].itemIds;
		for(var i = 0; i < number.length; i++ ){
			for (var BzEntry in BzEntries) {
				if(number[i] == BzEntry){
					var data = BzEntries[BzEntry].data;
					var newId = data.id;
					var bzTitle = data.bzTitle;
					var previewUrl = data.previewUrl;
					var description = data.description;
					var urlTitle = removeVietnameseTones(bzTitle);
					urlTitle = urlTitle.replace(/\s/g, "-");
					if(count==0){
						mainNewBody += '<div class="bz-main-new-img">'+
											'<a href="/tin-tuc-covid/-/view-pccovid-new/'+newId+'/'+urlTitle+'">'+
								            	'<img src="'+previewUrl+'" class="img-fluid bluezone-news-img">'+
								            '</a>'+
								        '</div>'+
								        '<div class="bz-main-new-title">'+
								        	'<a href="/tin-tuc-covid/-/view-pccovid-new/'+newId+'/'+urlTitle+'">'+bzTitle+'</a>'+
							            '</div>';
					}else{
						subNewBody += '<li>'+
								      	'<a href="/tin-tuc-covid/-/view-pccovid-new/'+newId+'/'+urlTitle+'">'+bzTitle+'</a>'+
						        	  '</li>';
					}
					count++;
				}
				
			}
		}
	}
	if(count!=0){
		$("#home-new-wrap").html(mainNewBody);
		$("#home-sub-new-wrap ul").html(subNewBody);
		$("#news-loading").hide();
		$("#app-new-wrap").show();
	}
}

var x = navigator.userAgent;

$(function() {
	function getMobileOperatingSystem() {		
	  var userAgent = navigator.userAgent || navigator.vendor || window.opera;
	    // Windows Phone must come first because its UA also contains "Android"
	    if (/windows phone/i.test(userAgent)) {
	        return "Windows Phone";
	    }
	    if (/android/i.test(userAgent)) {
	        return "Android";
	    }
	    // iOS detection from: http://stackoverflow.com/a/9039885/177710
	    if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
	        return "iOS";
	    }
	    return "unknown";
	}
	var checkBrowser = getMobileOperatingSystem();
	if(checkBrowser=="Android"){
		$(".apk-file-download").show();
		$("#popup-bluezone-android").show();
		$(".gg-play-download").show();
		$(".bluezone-home-banner-download-ggStore").css("float","none");
		$(".bluezone-download-ggStore").css("float","none");
//		if (userDevice == "Huawei") {
//			$(".bluezone-download-huaweiStore").css("float","none");
//			$(".bluezone-download-ggStore").css("display","none");
//			$(".bluezone-download-appStore").css("display","none");
//		} else {
//			$(".bluezone-download-huaweiStore").css("display","none");
//		}
		
		if(x.includes("HMSCore")) {	
			$(".bluezone-download-huaweiStore").css("float","none");
			$(".huawei-download").show();
			$(".bluezone-download-ggStore").css("display","none");
			$(".bluezone-download-appStore").css("display","none");
		} else {		
			$(".bluezone-download-huaweiStore").css("display","none");
		}
	}else if(checkBrowser=="iOS"){
		$("#popup-bluezone-ios").show();
		$(".app-store-download").show();
		$(".bluezone-home-banner-download-appStore").css("float","none");
		$(".bluezone-download-appStore").css("float","none");
	}else{
		$("#popup-bluezone-desktop").show();
		$(".app-store-download").show();
		$(".gg-play-download").show();
		$(".huawei-download").show();
	}
});