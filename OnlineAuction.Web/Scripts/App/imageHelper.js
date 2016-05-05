$(document).ready(function() {
    var imgHelper = function() {
        var self = this;

        self.imgSrc = ko.observable(window.img || '');

        self.onChange = function () {
            var file = $('#img_upload')[0].files[0];

            if (file && file.type.match('image.*')) {
                var reader = new FileReader();

                reader.onloadend = function () {
                    var byteArray = new Uint8Array(reader.result);
                    var blob = new Blob([byteArray]);
                    self.imgSrc(URL.createObjectURL(blob));
                }

                reader.readAsArrayBuffer(file);
            }
        };
    };

    ko.applyBindings(imgHelper);
});