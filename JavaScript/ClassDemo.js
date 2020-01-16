var UAModule;
(function (UAModule) {

    // All classes in this module are NOT on the global namespace - this is a best practice!

    var ClassOne = (function () {
        function ClassOne() {
            var self = this;    // disambiguate 'this'

            // Private members
            var priVar = 'I am a private variable';

            function NoAccess() {
                return "No access to me outside the class, cuz I'm a private function; I do have access to " + priVar;
            }

            // Public members
            self.pubVar = "I am a public variable";

            self.FullAccess = function () {
                return "I am public with access to " + self.pubVar + " and access to " + NoAccess();
            }
        }

        ClassOne.prototype.OnProtoChain = function () {
            // Via the 'this' object, access to every public member defined in ClassOne is accessible.  ('this'  == self in this case)
            return "I am on the prototype chain so I am shared amoungst all instances; FullAccess is an instance member.  I have access to " + this.FullAccess();
        };

        return ClassOne;
    })();
    UAModule.ClassOne = ClassOne;

    var ClassTwo = (function () {
        // Constructor
        function ClassTwo() {
        }

        // I will inherit from ClassOne
        ClassTwo.prototype = new ClassOne();

        return ClassTwo;
    })();
    UAModule.ClassTwo = ClassTwo;

})(UAModule || (UAModule = {}));

// Demo!

// Create new instance
var myClassOne = new UAModule.ClassOne();

// Try to access private variable
//alert(myClassOne.priVar);   // This will display 'undefined'

// Try to access private method
//alert(myClassOne.NoAccess()); // This will cause an exception: 'Uncaught TypeError:  undefined is not a function'

// Try to access public stuff (no brainer)
//alert(myClassOne.pubVar + " " + myClassOne.FullAccess());  // Notice that FullAccess can access all private members

// Try to access ClassOne.OnProtoChain from ClassTwo
var myClassTwo = new UAModule.ClassTwo();
alert(myClassTwo.OnProtoChain());
