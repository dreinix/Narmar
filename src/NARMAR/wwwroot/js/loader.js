var ngModule = angular.module("narmarFlights", ["ngMaterial", "ngMessages"]);

var loader = function (load) {
    load(ngModule);
}

ngModule.run(function ($rootScope,$http, $mdDialog, $compile) {
    $rootScope.title = "";
    $rootScope.loginVisible = true;
    $rootScope.currentUser = undefined;
    $rootScope.accessToken = localStorage["accessToken"];
    $rootScope.actualURL = "/Main/Home";

    $rootScope.toggleLogin = function (state) {
        $rootScope.loginVisible = state || !$rootScope.loginVisible;
        if ($rootScope.loginVisible) {
            $mdDialog.show({
                contentElement: '#loginDialog',
                parent: angular.element(document.body),
                clickOutsideToClose: false
            });
        } else {
            $mdDialog.hide();
        }
    }
    $rootScope.goTo = function (url) {
        $rootScope.actualURL = url;
        $http.get(url).then(function (res) {
            $("#viewport").html( $compile(res.data)($rootScope) );
        })
    }
});

ngModule.config(function ($mdThemingProvider) {
    $mdThemingProvider
    .theme("default")
    .primaryPalette("teal")
    .accentPalette("indigo");

});

ngModule.filter("firstWord",function () {
    return function (value) {
        return (value || "").split(" ")[0]
    }
})