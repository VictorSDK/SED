(function () {

    var module = angular.module("SEDApp");

    var participantController = function ($scope, $log) {
        $log.info("participantController");
        //$scope.setActive("participant");
    };

    module.controller("participantController", participantController);
}());