angular.module('narmarFlights.controllers', [])
.controller("AppController", ["$scope", "$mdDialog", "goTo", function ($scope, $mdDialog, goTo) {
    $scope.goTo = goTo;
    $scope.accessToken = undefined;
    $scope.currentUser = undefined;
    $scope.showLogin = function (ev) {
        $mdDialog.show({
            contentElement: '#loginDialog',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        });
    };
    $scope.loginSuccess = function(data){
        $scope.accessToken = data.token;
        $.ajax({
            url: "/api/user/" + data.uid,
            method: "get",
            success: function (data) {
                console.log(data);
                $scope.currentUser = data;
            }
        });
    }
    $scope.init = function (data) {
        if (data.token && data.uid) {
            $scope.accessToken = data.token;
            $.ajax({
                url: "/api/user/" + data.uid,
                method: "get",
                success: function (data) {
                    console.log(data);
                    $scope.currentUser = data;
                }
            });
        }
    }
}])
.controller("LoginController", ["$scope", "$mdDialog", function ($scope, $mdDialog) {
    $scope.loginData = {
        username: "",
        password: ""
    };
    $scope.hide = function () {
        $mdDialog.hide();
    }
    $scope.checkLogin = function () {
        return !!$scope.loginData.username && !!$scope.loginData.password;
    };
    $scope.doLogin = function () {
        $.ajax({
            url: "/api/login",
            method: "post",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify($scope.loginData),
            success: function (data) {
                $scope.loginSuccess(data);
                $scope.hide();
            }
        })
    }
}])


.controller("HomeIndexController", ["$scope", function ($scope) {
    $scope.vision = "Be the first Dominican travel agency with international renown, with quality services, which, to give the best experience to our customers, adapting to their needs, with the necessary advice to, on vacation, have a unique experience.";
    $scope.mission = "Being the leading travel agency in Latin America, with the largest variety of destinations, flights per day and with greater safety and comfort the market.";

    $(document).ready(function ($) {

        var jssor_1_options = {
            $AutoPlay: true,
            $SlideDuration: 800,
            $SlideEasing: $Jease$.$OutQuint,
            $CaptionSliderOptions: {
                $Class: $JssorCaptionSlideo$
            },
            $ArrowNavigatorOptions: {
                $Class: $JssorArrowNavigator$
            },
            $BulletNavigatorOptions: {
                $Class: $JssorBulletNavigator$
            }
        };

        var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

        //responsive code begin
        //you can remove responsive code if you don't want the slider scales while window resizing
        function ScaleSlider() {
            var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
            if (refSize) {
                refSize = Math.min(refSize, 1920);
                jssor_1_slider.$ScaleWidth(refSize);
            }
            else {
                window.setTimeout(ScaleSlider, 30);
            }
        }
        ScaleSlider();
        $(window).bind("load", ScaleSlider);
        $(window).bind("resize", ScaleSlider);
        $(window).bind("orientationchange", ScaleSlider);
        //responsive code end
    });

}])


.controller("UserIndexController", ["$scope", "$mdDialog", "$mdToast", "countryFactory", function ($scope, $mdDialog, $mdToast, countryFactory) {
    $scope.countries = countryFactory;
    $scope.details = function (id) {
        location.href = "/User/Details/" + id;
    };
    $scope.edit = function (id) {
        location.href = "/User/Edit/" + id;
    };
    $scope.delete = function (id, $event) {
        console.log("deleting ", id);
        $mdDialog.show(
            $mdDialog.confirm()
            .title("Confirm delete")
            .textContent("Are you sure you want to delete this user?")
            .targetEvent($event)
            .ok("Yes")
            .cancel("No")
        ).then(function () {
            $.ajax({
                method: "DELETE",
                url: "api/user/" + id,
                success: function () {
                    $mdToast.show(
                        $mdToast.simple()
                        .textContent("The user was deleted successfully")
                        .hideDelay(3000)
                    );
                    $("[data-user-id='" + id + "']").remove();
                }
            });
        });
    };
}])
.controller("UserDetailsController", ["$scope", "$mdDialog", "$mdToast", "countryFactory", "salaryRanges", function ($scope, $mdDialog, $mdToast, countryFactory, salaryRanges) {
    $scope.countries = countryFactory;
    $scope.salaryRanges = salaryRanges;
    // Initialization function
    $scope.init = function () {
        $scope.user.birthDate = new Date($scope.user.birthDate);
    };
    // Delete actual user
    $scope.delete = function (id, redirect, $event) {
        $mdDialog.show(
            $mdDialog.confirm()
            .title("Confirm delete")
            .textContent("Are you sure you want to delete this user?")
            .targetEvent($event)
            .ok("Yes")
            .cancel("No")
        ).then(function () {
            $.ajax({
                method: "DELETE",
                url: "/api/user/" + id,
                success: function () {
                    $mdToast.show(
                        $mdToast.simple()
                        .textContent("The user was deleted successfully")
                        .hideDelay(3000)
                    );
                    $scope.goTo(redirect);
                }
            });
        });
    };
}])
.controller("UserConfirmController",["$scope", function ($scope) {
    $scope.correo = "nada@nada.nada";
    $scope.Usuario = "El usuario"

    $scope.ConfirmCode = function (codigo) {
      
        if(angular.uppercase(codigo)=="A004"){
            console.log("Aceptado");
            document.getElementById('CodeConfirm').style.display = 'none';
            document.getElementById('CodeConfirmed').style.display = 'block';
        }
        else {
            console.log("codigo erroneo");
        }
    }
    $scope.ConfirmUSer = function (user,pass,conpass) {
        if (pass == conpass) {
            console.log("work");
            
        }
    }

}])

.controller("VueloController", ["$scope", "countryFactory", function ($scope, countryFactory) {
    $scope.countries = countryFactory;
   



    $scope.AvaibleFlight = [
      { "id": 0, "Ocity": "Romana", "Ocountry": "Rep. Dom", "Dcity": "Miami", "Dcountry": "U.S.A", "Cost": "400US", "Escalas": "Sin escala", "HoraS": "13:00", "HoraF": "15:30","Apeople":"40" },
      { "id": 1, "Ocity": "Miami", "Ocountry": "U.S.A", "Dcity": "Puerto Plata", "Dcountry": "Rep. Dom", "Cost": "400US", "Escalas": "Sin escala", "HoraS": "13:00", "HoraF": "15:30", "Apeople": "40" },
      { "id": 2, "Ocity": "Tokyo", "Ocountry": "Japan", "Dcity": "Tampa", "Dcountry": "U.S.A", "Cost": "400US", "Escalas": "Sin escala", "HoraS": "13:00", "HoraF": "15:30", "Apeople": "40" },
      { "id": 2, "Ocity": "Bonao", "Ocountry": "Rep. Dom", "Dcity": "Tampa", "Dcountry": "U.S.A", "Cost": "400US", "Escalas": "Sin escala", "HoraS": "13:00", "HoraF": "15:30", "Apeople": "40" },
      { "id": 2, "Ocity": "NY", "Ocountry": "U.S.A", "Dcity": "Tokyo", "Dcountry": "Japan", "Cost": "400US", "Escalas": "Sin escala", "HoraS": "13:00", "HoraF": "15:30", "Apeople": "40" },
      { "id": 2, "Ocity": "Irohchima", "Ocountry": "Japan", "Dcity": "Miami", "Dcountry": "U.S.A", "Cost": "400US", "Escalas": "Sin escala", "HoraS": "13:00", "HoraF": "15:30", "Apeople": "40" }
    ]

    $scope.DestinoActual = null;

    $scope.CurrentOrigin = null;
    $scope.CurrentDestination = null;


    $scope.GoTo = function (url) {
        location.href = url;
    }
    $scope.Flight = {
        Origin: "",
        Destination: "",
        CurrentFlight: "",
        Date: (function () {
            today = new Date();
            today.setFullYear(today.getFullYear());
            return today;
        })(),
        Adult: "",
        Teen: "",
        Kids: "",
        Baby: ""
            
    };

    this.Cards = ('Visa Master_Card').split(' ').map(function (Card) { return { Current: Card }; });

    
    $scope.Pay = {
        Fname: "",
        Lname: "",
        CardType: "",
        CardNumber: "",
        CCV: "",
        ExpirateDate: ""

    };

    $scope.formReady = function () {
        let a = !!$scope.Flight.Origin;
        let b = !!$scope.Flight.Destination;
        let c = !!$scope.Flight.Date;
        let d = !!$scope.Flight.Adult;
        let e = !!$scope.Flight.CurrentFlight;
        let f = !!$scope.pay.Fname;
        let g = !!$scope.pay.Lname;
        let h = !!$scope.pay.CardType;
        let i = !!$scope.pay.CardNumber;
        let j = !!$scope.pay.CCV;
        let k = !!$scope.pay.ExpirateDate;
        return (
            a && b && c && d &&
            e && f && g && h &&
            i && j && k
        );
    };
    $scope.DataReady = function () {
        let a = !!$scope.Flight.Origin;
        let b = !!$scope.Flight.Destination;
        let c = !!$scope.Flight.Date;
        let d = !!$scope.Flight.Adult;
        return (
            a && b && c && d 
        );
    };
    $scope.FlightReady = function () {
        let a = !!$scope.Flight.Origin;
        let b = !!$scope.Flight.Destination;
        let c = !!$scope.Flight.Date;
        let d = !!$scope.Flight.Adult;
        let e = !!$scope.Flight.CurrentFlight;
        return (
            a && b && c && d && e
        );
    };
    function SetOrigin(dest) {
        $scope.Flight.Origin = dest;
    }
    $scope.MinToday = function (date) {
        today = new Date();
        today.setFullYear(today.getFullYear());
        return date > today;
    };
    $scope.SetOrigin = SetOrigin;

    function SetDestination(dest) {
        $scope.Flight.Destination = dest;
    }
    $scope.SetFlight = SetFlight;
    function  SetFlight(flight) {
        $scope.Flight.CurrentFlight = flight;
    }

    $scope.SetDestination = SetDestination;



}])
.controller('UserCreateController', ["$scope", "$mdToast", "countryFactory", "salaryRanges", function ($scope, $mdToast, countryFactory, salaryRanges) {
    const locals = {
        tabCount: 3
    };
    // Define user default data:
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
    /* Form static public data */
    $scope.countries = countryFactory;
    $scope.salaryRanges = salaryRanges;

    /* Tab Manager */
    $scope.actualTab = 0;
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

    /* Form Validation */
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
        // Only Phone and Email are required, everything else can be deleted
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

    /* Form Controller */
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
}])
.controller('UserEditController', ["$scope", "$mdToast", "countryFactory", "salaryRanges", function ($scope, $mdToast, countryFactory, salaryRanges) {
    const locals = {
        tabCount: 3
    };
    $scope.init = function () {
        $scope.user.birthDate = new Date($scope.user.birthDate);
        $scope.user.country = countryFactory.getCountry($scope.user.country);
    };
    /* Form static public data */
    $scope.countries = countryFactory;
    $scope.salaryRanges = salaryRanges;

    /* Tab Manager */
    $scope.actualTab = 0;
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

    /* Form Validation */
    $scope.only18 = function (date) {
        today = new Date();
        today.setFullYear(today.getFullYear() - 18);
        return date < today;
    };
    $scope.canDeleteContact = function (type) {
        // Only Phone and Email are required, everything else can be deleted
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

    /* Form Controller */
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
        $scope.user.country = $scope.user.country.code;
        console.log(JSON.stringify($scope.user));
        $.ajax({
            url: url,
            data: JSON.stringify($scope.user),
            dataType: "json",
            contentType: "application/json",
            method: "put",
            success: function (data) {
                console.log("Success: ", data);
                $mdToast.show(
                    $mdToast.simple()
                    .textContent("Usuario enviado correctamente")
                    .hideDelay(3000)
                );
                $scope.goTo(redirect);
            },
            error: function (err) {
                console.log("Fail: ", data);
                $mdToast.show(
                    $mdToast.simple()
                    .textContent("Error: " + err.statusText)
                    .hideDelay(3000)
                );
            }
        });
    };
}]);