////var user = {
////    init: function () {

////        user.loadProvince();
////        user.registerEvent();

////    },
////    registerEvent: function () {
////        $('#ddlProvince').off('change').on('change', function () {
////            var name = $(this).val();
////            if (name != '') {
////                user.loadDistrict(name);
////                user.loadWards(name);
////            }
////            else {
////                $('#ddlDistrict').html('');
////                $('#ddlWards').html('');
////            }
////        });
////        $('#ddlDistrict').off('change').on('change', function () {
////            var name = $(this).val();
////            if (name != '') {
////                user.loadWards(name);
////            }
////            else {
////                $('#ddlWards').html('');
////            }
////        });
////    },


////    loadProvince: function () {

////        $.ajax({
////            url: '/KhaiBaoYTe/LoadProvince',
////            type: "POST",
////            dataType: "json",
////            success: function (response) {
////                if (response.status == true) {
////                    var html = '<option value="">-- Chọn tỉnh thành --</option>';
////                    var data = response.data;
////                    $.each(data, function (i, item) {
////                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
////                    });
////                    $('#ddlProvince').html(html);
////                }
////            }
////        })
////    },
////    loadDistrict: function (name) {
////        $.ajax({
////            url: '/KhaiBaoYTe/LoadDistrict',
////            type: "POST",
////            data: { provinceName: name },
////            dataType: "json",
////            success: function (response) {
////                if (response.status == true) {
////                    var html = '<option value="">-- Chọn quận huyện --</option>';
////                    var data = response.data;
////                    $.each(data, function (i, item) {
////                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
////                    });
////                    $('#ddlDistrict').html(html);
////                }
////            }
////        })
////    },
////    loadWards: function (name) {
////        $.ajax({
////            url: '/KhaiBaoYTe/LoadWards',
////            type: "POST",
////            data: { DistrictName: name },
////            dataType: "json",
////            success: function (response) {
////                if (response.status == true) {
////                    var html = '<option value="">-- Chọn xã phường --</option>';
////                    var data = response.data;
////                    $.each(data, function (i, item) {
////                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
////                    });
////                    $('#ddlWards').html(html);
////                }
////            }
////        })
////    }
////}
////user.init();









var user = {
    init: function () {

        user.loadProvince();
        user.registerEvent();

    },
    registerEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
                $('#ddlWards').html('');
              
            }
            else {
                $('#ddlDistrict').html('');
                
            }
        });
        $('#ddlDistrict').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadWards(parseInt(id));
            }
            else {
                $('#ddlWards').html('');
            }
        });
    },


    loadProvince: function () {

        $.ajax({
            url: '/KhaiBaoYTe/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">-- Chọn tỉnh thành --</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + "_" + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },
    loadDistrict: function (id) {
        $.ajax({
            url: '/KhaiBaoYTe/LoadDistrict',
            type: "POST",
            data: { provinceID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">-- Chọn quận huyện --</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID +"_"+ item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },
    loadWards: function (id) {
        $.ajax({
            url: '/KhaiBaoYTe/LoadWards',
            type: "POST",
            data: { DistrictID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">-- Chọn xã phường --</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' +item.ID +"_"+ item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlWards').html(html);
                }
            }
        })
    }
}
user.init();

