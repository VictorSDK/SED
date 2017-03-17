(function () {

    var module = angular.module("SEDApp");

    var activityController = function ($scope, $log, $routeParams) {
        $log.info("activityController");

        var activityCode = $routeParams.activityCode;

        $scope.activityCode = activityCode;
    };

    module.controller("activityController", activityController);

}());