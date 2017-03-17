(function () {
    var module = angular.module("SEDApp", ["ngRoute", "ngResource"]);

    module.config(function ($routeProvider) {
        console.log("$routeProvider");
        $routeProvider
            .when("/main", {
                templateUrl: "app/views/events-upcoming.html",
                controller: "upcomingEventsController"
            })
            .when("/activity/:activityCode", {
                templateUrl: "app/views/activity.html",
                controller: "activityController"
            })
            .when("/participant", {
                templateUrl: "app/views/participant.html",
                controller: "participantController"
            })
            .when("/host/event-create", {
                templateUrl: "app/views/event-creation.html",
                controller: "hostController"
            })
            .when("/host", {
                templateUrl: "app/views/host.html",
                controller: "hostController"
            })
            .when("/event/:sportEventId", {
                templateUrl: "app/views/event-detail.html",
                controller: "eventController"
            })
            .when("/host/event-detail/:sportEventId", {
                templateUrl: "app/views/event-detail-host.html",
                controller: "sportEventDetailController"
            })
            .when("/host/event-edit/:sportEventId", {
                templateUrl: "app/views/event-edition.html",
                controller: "sportEventDetailController"
            })
            .when("/host/notification/:sportEventId", {
                templateUrl: "app/views/notification.html",
                controller: "notificationController"
            })
            .when("/login", {
                    templateUrl: "app/views/login.html",
                controller: "loginController"
            })
            .otherwise({ redirectTo: "/main" });
    });
    
}());
