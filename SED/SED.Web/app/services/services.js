(function () {
    var module = angular.module("SEDApp");

    var services = function ($resource, $log) {
        $log.info("services");

        var serviceBase = 'http://localhost:9000/api/';

        var getUpcomingEvents = function () {
            return $resource(serviceBase + "SportEvents/GetUpcomming").query();
        };

        var searchSportEvents = function (searchParams) {
            return $resource(serviceBase + "SportEvents/Search").query(searchParams);
        };

        var getEventDetails = function (sportEventId) {
            return $resource(serviceBase + "SportEvents/" + sportEventId).get();
        };

        var saveEvent = function (sportEvent) {
            return $resource(serviceBase + 'SportEvents/Create').save(sportEvent);
        };

        var updateEvent = function (sportEvent) {
            return $resource(serviceBase + 'SportEvents', null
                  , { update: { method: 'PUT' } })
                .update(sportEvent);
        };

        var deleteEvent = function (sportEventId) {
            return $resource(serviceBase + 'SportEvents/' + sportEventId).remove();
        };

        var getActivityTypes = function () {
            return $resource(serviceBase + "ActivityTypes").query();
        };

        var subscribe = function(subscriber) {
            return $resource(serviceBase + 'SportEvents/subscribe').save(subscriber);
        };

        var notify = function (email) {
            return $resource(serviceBase + 'host/notify').save(email);
        };

        return {
            getUpcomingEvents: getUpcomingEvents,
            searchSportEvents: searchSportEvents,
            getEventDetails: getEventDetails,
            saveEvent: saveEvent,
            updateEvent: updateEvent,
            getActivityTypes: getActivityTypes,
            deleteEvent: deleteEvent,
            subscribe: subscribe,
            notify: notify

        };
    };

    module.factory("services", ["$resource", "$log", services]);
}());
