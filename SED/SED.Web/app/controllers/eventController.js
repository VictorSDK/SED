(function () {

    var module = angular.module("SEDApp");

    var eventController = function ($scope, $log, $routeParams, services) {
        $log.info("eventController");
        //$scope.setActive("upcomingEvents");

        $scope.email = "";
        $scope.subscriptionFeedback = "";
        $scope.close = false;
        $scope.subscribe = function () {
            var subscriber = {
                email: $scope.email,
                sportEventId: $scope.sportEvent.sportEventId
            };
            services.subscribe(subscriber).$promise.then(
                function () {
                    $scope.subscriptionFeedback = "Subscribed sucessfully";
                    $scope.close = true;
                },
                function (error) {
                    $scope.subscriptionFeedback = "Something went wrong with subscription: " + error.data.message;
                });
        };

        //var onGetEventComplete = function (data) {
        //    $scope.sportEvent = maptoUIModel(data);
        //};

        var maptoUIModel = function (data) {
                var starsRating = { count: 5, rating: data.rating, stars: [] };
                for (var j = 1; j <= starsRating.count; j++) {
                    starsRating.stars.push(j > data.rating);
        }
                data.starsRating = starsRating;
                delete data.rating;
                if (data.hasOwnProperty("comments")) {
                    for (var i = 0; i < data.comments.length; i++) {
                        maptoUIModel(data.comments[i]);
        }
        }
                return data;
    };

        //var onError = function (reason) {
        //    $scope.error = "Could not fetch data.";
        //    $log.error(reason);
        //};

        //services.getEventDetails($routeParams.sportEventId).then(onGetEventComplete, onError);

        $scope.error = false;
        services.getEventDetails($routeParams.sportEventId).$promise.then(
            function (data) {
                $scope.sportEvent = maptoUIModel(data);
                },
            function () {
                $scope.error = true;
    }
        );

    };

    module.controller("eventController", ["$scope", "$log", "$routeParams", "services", eventController]);


}());