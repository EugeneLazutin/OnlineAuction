(function () {

    $.connection.hub.start();

    var betBox = $('#bet-box');

    var hub = $.connection.auctionHub;

    var hideBetBox = function () {
        betBox.addClass('hidden');
    };

    if (!window.model) {
        hideBetBox();
        throw new Error('Model not found.');
    }

    var auction = function () {
        var self = this;

        var model = window.model;
        // ReSharper disable once JoinDeclarationAndInitializerJs
        var timer;
        var calculateTime = function () {
            var now = new Date(Date.now());
            if (now > model.endDate) {
                hideBetBox();
                clearInterval(timer);
                return "finished";

            } else if (now < model.startDate) {
                hideBetBox();
                return "not started";
            }
            if (betBox.hasClass('hidden')) {
                betBox.removeClass('hidden');
            }

            var date = new Date(model.endDate - now);

            return new Date(date.valueOf() + date.getTimezoneOffset() * 60000);
        };

        self.timeLeft = ko.observable(calculateTime());

        self.price = ko.observable(model.price);

        self.priceFormated = ko.computed(function () {
            return self.price() + ' $';
        });

        self.factor = ko.observable('0,05');

        self.getTimeLeft = ko.computed(function () {
            if (self.timeLeft() instanceof Date) {

                

                var output = '';
                var hours = self.timeLeft().getHours();
                var minutes = self.timeLeft().getMinutes();
                var seconds = self.timeLeft().getSeconds();

                if (minutes < 10)
                    minutes = '0' + minutes;

                if (seconds < 10)
                    seconds = '0' + seconds;

                if (hours !== 0) {
                    output += hours + ':';
                }

                return output + minutes + ':' + seconds;
            }
            return self.timeLeft();
        });

        hub.client.notify = function (price, date) {
            self.price(price);
            model.endDate = new Date(date);
        };

        self.makeBet = function () {
            $.ajax({
                type: "POST",
                url: "/WinLot/MakeBet/",
                data: { id: model.winLotId, factor: self.factor() }
            });
        };
        timer = setInterval(function () {
            self.timeLeft(calculateTime());
        }, 1000);
    };

    ko.applyBindings(auction);
})();