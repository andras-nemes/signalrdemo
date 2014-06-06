(function () {
    var resultsHub = $.connection.resultsHub;
    $.connection.hub.logging = true;
    $.connection.hub.start();

    $.connection.hub.start().done(function ()
    {        
        var age = $("#ageSpan").html();
        resultsHub.server.joinAppropriateRoom(age);
    });    

    resultsHub.client.hello = function(){
        
    }    

    resultsHub.client.registerMessage = function (message) {
        viewModel.addMessageToList(message);
    };

    resultsHub.client.newStockPrices = function (stockPrices) {        
        viewModel.addStockPrices(stockPrices);        
    }

    var messageModel = function () {
        this.registeredMessage = ko.observable(""),
        this.registeredMessageList = ko.observableArray(),
        this.stockPrices = ko.observableArray()
    };

    var stock = function (stockName, price) {
        this.stockName = stockName;
        this.price = price;
    };

    messageModel.prototype = {

        newMessage: function () {
            var age = $("#ageSpan").html();
            //resultsHub.server.sendMessage(this.registeredMessage());
            resultsHub.server.dispatchMessage(this.registeredMessage(), age);
            this.registeredMessage("");
        },
        addMessageToList: function (message) {
            this.registeredMessageList.push(message);
        },
        addStockPrices: function (updatedStockPrices) {
            var self = this;           

            $.each(updatedStockPrices, function (index, updatedStockPrice) {
                var stockEntry = new stock(updatedStockPrice.name, updatedStockPrice.price);
                self.stockPrices.push(stockEntry);                
            });
        }
    };

    var viewModel = new messageModel();
    $(function () {
        ko.applyBindings(viewModel);
    });
}());