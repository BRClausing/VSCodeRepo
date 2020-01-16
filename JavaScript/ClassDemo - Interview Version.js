var App;
(function (App) {

    var ClassOne = (function () {
        function ClassOne() {
            var self = this;
            var aVarOne = 'I am aVarOne';

            function FuncOne() {
                return "I am FuncOne " + aVarOne;
            }

            self.aVarTwo = "I am aVarTwo";

            self.FuncTwo = function () {
                return "I am FuncTwo " + self.aVarTwo + " and here is " + FuncOne();
            }
        }

        ClassOne.prototype.OnProtoChain = function () {
            return "I am on the prototype chain.  I have access to " + this.FuncTwo();
        };

        return ClassOne;
    })();
    App.ClassOne = ClassOne;

    var ClassTwo = (function () {

        function ClassTwo() {
        }

        ClassTwo.prototype = new ClassOne();

        return ClassTwo;
    })();
    App.ClassTwo = ClassTwo;

})(App || (App = {}));

