// Write your Javascript code.

angular.module("narmarFlights", ["ngMaterial", "ngMessages", "narmarFlights.controllers", "narmarFlights.factories"])
.constant("TABS_COUNT", 3)
.config(["$mdThemingProvider", function ($mdThemingProvider) {
    $mdThemingProvider
    .theme("default")
    .primaryPalette("teal")
    .accentPalette("indigo");
}])
.filter("getEmail", function () {
    return function (user) {
        if (user === undefined)
            return null;
        return user.ContactMethods.find(function (val) { return val.Type === "email" }).Content;
    }
});