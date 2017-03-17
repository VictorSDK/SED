(function () {
    var module = angular.module("SEDApp");

    var navigationController = function ($scope, $log, $location) {
        $log.info("navigationController");

        //$scope.setActive = function (type) {
        //    $log.info("setActive: " + type);
        //    $scope.upcomingEventsActive = "";
        //    $scope.participantActive = "";
        //    $scope.hostActive = "";

        //    $scope[type + "Active"] = "active";
        //};

        $scope.isActive = function (path) {
            return $location.path().substr(0, path.length) === path;
        };
    };

    module.controller("navigationController", navigationController);
    
}());