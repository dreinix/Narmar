// Write your Javascript code.

angular.module("narmarFlights", ["ngMaterial", "ngMessages", "narmarFlights.controllers", "narmarFlights.factories"])
.constant("TABS_COUNT", 3)
.config(["$mdThemingProvider", function ($mdThemingProvider) {
    $mdThemingProvider
    .theme("default")
    .primaryPalette("teal")
    .accentPalette("indigo");
}]);