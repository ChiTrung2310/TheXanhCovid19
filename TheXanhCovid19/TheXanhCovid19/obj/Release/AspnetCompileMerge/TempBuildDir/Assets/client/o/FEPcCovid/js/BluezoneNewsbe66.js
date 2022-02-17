function formatDateToString(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear(),
        hour = d.getHours(),
        min = d.getMinutes(),
        sec = d.getSeconds();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    if (hour.length < 2) hour = '0' + hour;
    if (min.length < 2) min = '0' + min;
    if (sec.length < 2) sec = '0' + sec;
		var text = hour+":"+min+":"+sec+" | "+day + "-"+month+"-"+year;
    return text;
}
function rentNewInfo(info, pagingURL){
	var jsonInfo = JSON.parse(info);
	var apiResult = jsonInfo.apiResult;
	apiResult = JSON.parse(apiResult);
	var newApiResult = jsonInfo.newApiResult;
	newApiResult = JSON.parse(newApiResult);
	var BzEntries = apiResult.BzEntry;
	var NewEntry = newApiResult.BzEntry;
	var HasBzEntries = apiResult.HasBzEntry;
	var htmlBody = "";
	var count=0;
	for (var newInfo in NewEntry) {
		var newData = NewEntry[newInfo].data;
		
		var newTitle = newData.bzTitle;
		var content = newData.content;
		var unixTime = newData.createdTime;
		var date = new Date(unixTime);
		var displayDate = formatDateToString(date);
		
		$(".articleView-journal-head").html(newTitle);
		$("#new-displayDate").html(displayDate);
		$(".articleView-journal-content").html(content);
	}
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
						htmlBody += '<div class="articleView-other-wrap">'+
										'<ul class="articleView-other-ul">'+
							            	'<li><div class="articleView-other-title">'+
							            		'<a href="/'+pagingURL+'/-/view-pccovid-new/'+newId+'/'+urlTitle+'">'+bzTitle+'</a>'+
							            	'</div></li>'+
							        	'</ul>'+
					            	'</div>';
						}
				count++;
			}
		}
	}
	if(count!=0){
		$(".articleView-others-wrap").append(htmlBody);
		$("#news-loading").hide();
		$("#app-new-wrap").show();
	}
}
function rentListNews(listNew, pagingURL){
	var jsonListNew = JSON.parse(listNew);
	var BzEntries = jsonListNew.BzEntry;
	var HasBzEntries = jsonListNew.HasBzEntry;
	var htmlBody = "";
	var count=0;
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
						
						htmlBody += '<div class="articleView-wrap">'+
											'<div class="row">'+
								            	'<div class="col-12 col-bluezone-title-mobile">'+
								            		'<a class="articleView-mobile-title" href="/'+pagingURL+'/-/view-pccovid-new/'+newId+'/'+urlTitle+'">'+bzTitle+'</a>'+
								            	'</div>	'+
								        	'</div>'+
									        '<div class="row">'+
									        	'<div class="col-lg-3 col-4 col-bluezone-img-mobile">'+
									        		'<img class="img-fluid articleView-img" src="'+previewUrl+'">'+
								            	'</div>'+
								            	'<div class="col-lg-9 col-8">'+
									            	'<div class="articleView-title-wrap">'+
									            		'<a class="articleView-title" href="/'+pagingURL+'/-/view-pccovid-new/'+newId+'/'+urlTitle+'">'+bzTitle+'</a>'+
									            	'</div>'+
									        		'<div class="articleView-summary">'+description+'</div>'+
								            	'</div>'+
							            	'</div>'+
						            	'</div>';
					}
				count++;
			}
		}
	}
	if(count!=0){
		$("#app-new-wrap").html(htmlBody);
		$("#news-loading").hide();
		$("#app-new-wrap").show();
	}
}