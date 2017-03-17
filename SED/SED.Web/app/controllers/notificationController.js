(function () {

    var module = angular.module("SEDApp");

    var notificationController = function ($scope, $log, $routeParams, $location, services) {
        $log.info("notificationController");

        $scope.email = {
            sportEventId: $routeParams.sportEventId,
        };

        $scope.send = function () {
            services.notify($scope.email).$promise.then(
                function () {
                    $scope.subscriptionFeedback = "Notification sent sucessfully";
                    $location.path('/host');
                },
                function (error) {
                    $scope.subscriptionFeedback = "Something went wrong with subscription: " + error.data.message;
                });
        };

        $scope.cancel = function () {
            $location.path('/host');
        };
    };

    module.controller("notificationController", ["$scope", "$log", "$routeParams", "$location", "services", notificationController]);


}());