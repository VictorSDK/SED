(function () {
    var module = angular.module("SEDApp");

    var sportEventDetailController = function ($scope, $log, $routeParams, $location, services) {
        $log.info("sportEventDetailController");

        $scope.error = false;
        function getErrors(response) {
            if (response.data.constructor === Array) {
                $scope.errors = response.data;
                return;
            }
            $scope.errors.push(response.data.message);
        }

        // Sport Event
        services.getEventDetails($routeParams.sportEventId).$promise.then(
            function (data) {
                $scope.sportEvent = data;
                $scope.sportEvent.date = new Date($scope.sportEvent.date);
            },
            function () { $scope.error = true; }
        );
       
        // Sport Event delete
        $scope.delete = function () {
            services.deleteEvent($scope.sportEvent.sportEventId).$promise.then(
                function () {
                    $location.path('/host');
                },
                function (response) {
                    getErrors(response);
                }
            );
        };
        
        // Sport Event cancel edit
        $scope.cancel = function () {
            $location.path('/host/event-detail/' + $scope.sportEvent.sportEventId);
        };
        
        // Sport Event save
        $scope.save = function () {
            $scope.errors = [];
            services.updateEvent($scope.sportEvent).$promise.then(
                function () {
                    $location.path('/host/event-detail/' + $scope.sportEvent.sportEventId);
                },
                function (response) {
                    getErrors(response);
                }
            );
        };

        // Activities
        $scope.activity = null;
        
        services.getActivityTypes().$promise.then(
            function (data) {
                $scope.activityTypes = data;
            },
            function (response) {
                getErrors(response);
            }
        );
        
        //Activity add
        $scope.add = function () {
            $scope.sportEvent.activities.push({
                activityId: 0,
                sportEventId: $scope.sportEvent.sportEventId,
                activityTypeId: 0,
                name: "",
                activityType: { activityTypeId: 0, name: null, description: null },
                showEdit: true
            });
        };

        // Activity edit
        $scope.edit = function (activity) {
            activity.originalActivityType = JSON.parse(JSON.stringify(activity.activityType));
            activity.showEdit = true;
        };

        $scope.okEdit = function (activity) {
            delete activity.originalActivityType;
            activity.showEdit = false;
        };

        $scope.cancelEdit = function (activity) {
            activity.activityType = activity.originalActivityType;
            if (!activity.activityType) {
                $scope.remove(activity);
                return;
            }
            delete activity.originalActivityType;
            activity.showEdit = false;
        };

        // Activity remove
        $scope.remove = function (activity) {
            var i = 0;
            for (; i < $scope.sportEvent.activities.length; i++) {
                if ($scope.sportEvent.activities[i].activityId === activity.activityId) {
                    break;
                }
            }
            $scope.sportEvent.activities.splice(i, 1);
        };

    };
    module.controller("sportEventDetailController", ["$scope", "$log", "$routeParams", "$location", "services", sportEventDetailController]);
}());