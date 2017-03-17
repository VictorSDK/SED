(function () {
    var module = angular.module("SEDApp");

    var hostController = function ($scope, $log, $location, services) {
        $log.info("hostController");
        //$scope.setActive("host");

        //var onUpcomingEventsComplete = function (data) {
        //    $scope.sportEvents = data;
        //};

        //var onError = function (reason) {
        //    $scope.error = "Could not fetch data.";
        //    $log.error(reason);
        //};

        $scope.save = function (sportEvent) {
            $scope.errors = [];
            services.saveEvent(sportEvent).$promise.then(
                function () { $location.url("host"); },
                function (response) {
                    $scope.errors = response.data;
                }
            );
        };

        //services.getUpcomingEvents().then(onUpcomingEventsComplete, onError);

        $scope.sportEvents = services.getUpcomingEvents();
        
    };
    module.controller("hostController", ["$scope", "$log", "$location", "services", hostController]);
}());