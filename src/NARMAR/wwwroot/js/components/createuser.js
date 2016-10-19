loader(function (ngModule) {
    ngModule.directive('narmarCreateUser', () => ({
        templateUrl: "/Main/CreateUser",
        restrict: 'E',
        scope: {
            
        },
        controller: function ($scope, $rootScope, $http, $mdDialog, $mdToast, countryFactory, salaryRanges) {
            /*** Data ***/
            $scope.$rootScope = $rootScope;
            $scope.user = {
                firstName: "",
                lastName: "",
                country: "",
                birthDate: (function () {
                    today = new Date();
                    today.setFullYear(today.getFullYear() - 18);
                    return today;
                })(),
                gender: "male",
                civilStatus: "single",
                passport: "",
                identityDocument: "",
                contactMethods: [{
                    type: "phone",
                    content: ""
                }, {
                    type: "email",
                    content: ""
                }],
                job: {
                    company: "",
                    department: "",
                    position: "",
                    salary: 0
                }
            };
            $scope.countries = countryFactory;
            $scope.salaryRanges = salaryRanges;
            $scope.actualTab = 0;
            /*** Functions ***/
            $scope.setTab = function (tab) {
                $scope.actualTab = tab;
            };
            $scope.checkTab = function (tab) {
                return $scope.actualTab === tab;
            };
            $scope.nextTab = function () {
                if ($scope.actualTab < locals.tabCount - 1)
                    $scope.actualTab++;
            };
            $scope.prevTab = function () {
                if ($scope.actualTab > 0)
                    $scope.actualTab++;
            };
            $scope.capitalizeName = function () {
                $scope.user.firstName = $scope.user.firstName.replace(/(\b[a-z](?!\s))/g, function (x) { return x.toUpperCase(); });
                $scope.user.lastName = $scope.user.lastName.replace(/(\b[a-z](?!\s))/g, function (x) { return x.toUpperCase(); });
            };
            $scope.countryChanged = function (item) {
                $scope.user.country = item ? item.code : "";
            };
            $scope.only18 = function (date) {
                today = new Date();
                today.setFullYear(today.getFullYear() - 18);
                return date < today;
            };
            $scope.canDeleteContact = function (type) {
                if (!["phone", "email"].some((t) => { return t === type; }))
                    return true;

                let items = $scope.user.contactMethods.filter((val) => {
                    return val.type === type;
                });
                return items.length > 1;
            };
            $scope.formReady = function () {
                let a = !!$scope.user.firstName;
                let b = !!$scope.user.lastName;
                let c = !!$scope.user.passport;
                let d = !!$scope.user.country;
                let e = !!$scope.user.contactMethods.every((val) => { return !!val.content; });
                return (
                    a && b &&
                    c && d && e
                );
            };


            $scope.canAddContacts = function () {
                return $scope.user.contactMethods.length < 10;
            };
            $scope.addContact = function () {
                $scope.user.contactMethods.push({
                    type: "phone",
                    content: ""
                });
            };
            $scope.removeContact = function (idx) {
                $scope.user.contactMethods.splice(idx, 1);
            };
            $scope.submitUser = function (url, redirect) {
                console.log(JSON.stringify($scope.user));
                $.ajax({
                    url: url,
                    data: JSON.stringify($scope.user),
                    dataType: "json",
                    contentType: "application/json",
                    method: "post",
                    success: function (data) {
                        $mdToast.show(
                            $mdToast.simple()
                            .textContent("Usuario enviado correctamente")
                            .hideDelay(3000)
                        );
                        $scope.goTo(redirect);
                    },
                    error: function (err) {
                        $mdToast.show(
                            $mdToast.simple()
                            .textContent("Error: " + err.statusText)
                            .hideDelay(3000)
                        );
                    }
                });
            };

            /*** Main ***/
            
        }
    }));
});