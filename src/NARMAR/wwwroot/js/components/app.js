loader(function (ngModule) {
    ngModule.directive('narmarApp', () => ({
        templateUrl: "/Component/App",
        restrict: 'E',
        scope: {

        },
        controller: function ($scope, $rootScope, $mdDialog) {
            /*** Data ***/
            $scope.$rootScope = $rootScope;
            $scope.vision = "Be the first Dominican travel agency with international renown, with quality services, which, to give the best experience to our customers, adapting to their needs, with the necessary advice to, on vacation, have a unique experience.";
            $scope.mission = "Being the leading travel agency in Latin America, with the largest variety of destinations, flights per day and with greater safety and comfort the market.";

            /*** Functions ***/

            /*** Main ***/

        }
    }));
});