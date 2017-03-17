(function () {

    var module = angular.module("SEDApp");

    var upcomingEventsController = function ($scope, $log, services) {
        $log.info("upcomingEventsController");
        //$scope.setActive("upcomingEvents");
        

        //var onUpcomingEventsComplete = function (data) {
        //    $scope.sportEvents = maptoUIModel(data);
        //};

        //var onSearchComplete = function (data) {
        //    $scope.sportEvents = maptoUIModel(data);
        //}

        var maptoUIModel = function (data) {
            for (var i = 0; i < data.length; i++) {
                var starsRating = { count: 5, rating: data[i].rating, stars: [] };
                for (var j = 1; j <= starsRating.count; j++) {
                    starsRating.stars.push(j > data[i].rating);
                }
                data[i].starsRating = starsRating;
                delete data[i].rating;
            }
            return data;
        };

        //var onError = function (reason) {
        //    $scope.error = "Could not fetch data.";
        //    $log.error(reason);
        //};

        
        $scope.search = function () {
            $log.info("search" + $scope.searchParams);
            $scope.error = false;
            services.searchSportEvents($scope.searchParams).$promise.then(
                    function (data) {
                        $scope.sportEvents = maptoUIModel(data);
                    },
                    function (error) {
                        $scope.error = true;
                        console.log(error);
                    }
            );
            //services.searchSportEvents($scope.searchParams).then(onSearchComplete, onError);
        };


        $scope.error = false;
        services.getUpcomingEvents().$promise.then(
            function (data) {$scope.sportEvents = maptoUIModel(data); },
            function () { $scope.error = true; }
        );        
        //services.getUpcomingEvents().then(onUpcomingEventsComplete, onError);

    };

    module.controller("upcomingEventsController", ["$scope", "$log", "services", upcomingEventsController]);

    
}());