$(document).ready(function () {

    if (!window.model) {
        throw new Error("Model not found.");
    }

    var dateFormat = function (date) {
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();

        if (month < 10)
            month = '0' + month;

        if (day < 10)
            day = '0' + day;

        return year + '-' + month + '-' + day;
    };

    var timeFormat = function (date) {
        var hours = date.getHours();
        var minutes = date.getMinutes();

        if (minutes < 10)
            minutes = '0' + minutes;

        return hours + ':' + minutes;
    };

    var dateEl = $('#date')[0];
    var timeEl = $('#time')[0];

    var now = new Date(Date.now());

    dateEl.value = dateFormat(now);
    timeEl.value = timeFormat(now);
    

    var dateHelper = function () {
        var self = this;

        

        var time = window.model.time;

        var format = function (date) {
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var hours = date.getHours();
            var minutes = date.getMinutes();

            if (month < 10)
                month = '0' + month;

            if (day < 10)
                day = '0' + day;

            if (minutes < 10)
                minutes = '0' + minutes;

            return day + '.' + month + '.' + year + ' ' + hours + ':' + minutes;
        }

        var submitBtn = $('input[type=submit');
        var msgLabel = $('#msg');

        self.onChange = function () {
            if (dateEl.value && timeEl.value) {
                self.date(new Date(dateEl.value + ' ' + timeEl.value));
                submitBtn.removeAttr('disabled');
                msgLabel.text('');
            } else {
                submitBtn.attr('disabled', 'disabled');
                msgLabel.text('Enter start date and time.');
            }
        };

        self.date = ko.observable(now);

        self.startDate = ko.computed(function() {
            return format(self.date());
        });

        self.endDate = ko.computed(function () {
            var date = new Date(self.date().setMinutes(self.date().getMinutes() + time));
            return format(date);
        });

        
    };

    ko.applyBindings(dateHelper);
});