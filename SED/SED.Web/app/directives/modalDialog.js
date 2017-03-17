(function () {
    var module = angular.module("SEDApp");

    var modal = function () {
        return {
            restrict: 'EA',
            scope: {
                title: '@',
                body: '@',
                footer: '@',
                buttonLeftText: '@',
                buttonRightText: '@',
                callbackbuttonright: '&ngClickRightButton',
                handler: '=lolo'
                },
            templateUrl: 'app/views/modalDialog.html',
            transclude: true,
            controller: function ($scope) {
                $scope.handler = 'pop';
            },
        };
    };
    module.directive('modal', modal);
}());

(function () {
    var module = angular.module("SEDApp");

    var modalSubscribe = function () {
        return {
            restrict: 'EA',
            scope: {
                title: '@',
                email: '=',
                footer: '=',
                close: '=',
                buttonLeftText: '@',
                buttonRightText: '@',
                buttonCloseText: '@',
                callbackbuttonright: '&ngClickRightButton',
                handler: '=lolo'
            },
            templateUrl: 'app/views/modalSubscribe.html',
            transclude: true,
            controller: function ($scope) {
                $scope.handler = 'popSubscribe';
            },
        };
    };
    module.directive('modalSubscribe', modalSubscribe);
}());