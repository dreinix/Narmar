loader(function (ngModule) {
    ngModule.directive('narmarToolbar', () => ({
        templateUrl: "/Component/Toolbar",
        restrict: 'E',
        scope: {
            handleLoginToggle: "&onLoginToggle"
        },
        controller: function ($scope,$rootScope,$http,$mdToast) {
            /*** Data ***/
            $scope.$rootScope = $rootScope;

            /*** Functions ***/
            $scope.showMenu = function ($mdOpenMenu, ev) {
                $mdOpenMenu(ev);
            }
            // end actual session
            $scope.logout = function () {
                $http.delete(`/api/login/${$rootScope.accessToken}`)
                .then(function (res) {
                    delete localStorage["accessToken"];
                    $rootScope.accessToken = undefined;
                    $rootScope.currentUser = undefined;
                })
                .catch(function (err) {
                    console.log("Error:", err);
                    $mdToast.show($mdToast.simple().textContent("Error: " + JSON.stringify(err)));
                });
            }

            /*** Main ***/

        }
    }));
});