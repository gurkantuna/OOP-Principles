using GurkanTuna.Encapsulation;
using GurkanTuna.Inheritance;
using GurkanTuna.Polymorphism;

namespace GurkanTuna.OOP_Principles {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("**************** ENCAPSULATION ****************");
            var employee = new Employee("John", "Doe", DateTime.Now.AddYears(-30));

            var account = new Account { Balance = 1000 };
            var withdrawAmount = 300;
            if (account.Withdraw(withdrawAmount)) {
                Console.WriteLine($"${withdrawAmount} withdrawn. New balance is ${account.Balance}");
            }
            withdrawAmount = 5000;
            account.Withdraw(withdrawAmount);

            Console.WriteLine("\n**************** ABSTRACTION ****************");
            var car = new Abstraction.Car(100, 5) {
                Speed = 180,
                Gear = 3//Even though we shift to 3rd gear at 180 km/h, the vehicle shifts to 5th gear.
            };
            Console.WriteLine($"Speed : {car.Speed} km/h, Gear : {car.Gear}");

            var car2 = new Abstraction.Car(250) {
                Speed = 180,
                Gear = 3//At 180 km/h, the vehicle shifts into 3rd gear without any problems.
            };
            Console.WriteLine($"Speed : {car2.Speed} km/h, Gear : {car2.Gear}");

            Console.WriteLine("\n**************** INHERITANCE ****************");
            var star = new Star();
            Console.WriteLine(star.Info);
            var planet = new Planet();
            Console.WriteLine(planet.Info);
            var sun = new Sun();
            Console.WriteLine(sun.Info);

            Console.WriteLine("\n**************** POLYMORPHISM ****************");
            var car3 = new Polymorphism.Car();
            var boat = new Boat();
            var plane = new Plane();

            Console.WriteLine($"{car3.Start()}\n{boat.Start()}\n{plane.Start()}\n");
            Console.WriteLine($"{car3.Move()}\n{boat.Move()}\n{plane.Move()}\n");
            Console.WriteLine($"{boat.Swim()}\n{plane.Fly()}");

            Console.Read();
        }
    }
}

namespace GurkanTuna.Encapsulation {
    class Employee {
        public Employee(string firstName, string lastName, DateTime birthDate) {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.DateOfRecruitment = DateTime.Now;
            Console.WriteLine($"{FirstName} {LastName} ({Age}) {DateOfRecruitment:d} started the work.");
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfRecruitment { get; private set; }

        public DateTime BirthDate { get; protected set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;
    }

    class Account {
        public required decimal Balance { get; set; }

        public bool Withdraw(decimal amount) {
            var result = false;
            if (Balance <= 0 || amount > Balance) {
                Console.WriteLine($"There is ${Balance} amount in your account.\nYou must put down minimum ${Balance * -1 + amount} to withdraw ${amount}");
            }
            else if (amount > Balance) {
                Console.WriteLine("There is not enough money in your account!");
            }
            else {
                Balance -= amount;
                result = true;
            }
            return result;
        }
    }
}

namespace GurkanTuna.Inheritance {

    class Planet : CelestialBody {

        public decimal Orbit { get; set; }

        public decimal Gravity { get; set; }

        public decimal Radius { get; set; }

        public decimal RotationSpeed { get; set; }
    }
    class Galaxy {
        public HashSet<Planet> CelestialBodies { get; set; } = [];
    }

    class CelestialBody {

        public CelestialBody() {
            Info = $"{this.GetType().Name} created";
        }

        public string Name { get; set; }
        public decimal Mass { get; set; }
        public decimal KnownLifeTime { get; set; }
        public string Info { get; }
    }

    class Star : CelestialBody {
        public Star() {
            /*Stars generally consist of hydrogen and helium. But since there is no certainty, we leave the list as writeable.*/
            Content = ["Hidrojen", "Helyum"];
        }
        public string[] Content { get; set; }
    }

    class Sun : Star {
        public HashSet<Planet> Planets { get; set; } = [];
    }


    abstract class VehicleBase {
        public string Model { get; set; }

        public abstract VehicleUsageType UsageType { get; }
    }

    class LightVehicle : VehicleBase {
        public override VehicleUsageType UsageType => VehicleUsageType.Private;
    }

    class HeavyVehicle : VehicleBase {
        public decimal MaximumPayload { get; set; }

        public override VehicleUsageType UsageType => VehicleUsageType.Commercial;
    }

    class Car : LightVehicle {

    }

    class Truck : HeavyVehicle {

    }

    enum VehicleUsageType {
        None,
        Private,
        Commercial
    }
}

namespace GurkanTuna.Abstraction {
    abstract class VehicleBase {

        protected VehicleBase(int horsePower, int? maxGear) {
            HorsePower = horsePower;
            MaxGear = maxGear ?? 5;
        }

        public int MaxGear;
        public abstract int Gear { get; set; }
        public int HorsePower { get; }
        public int Speed { get; set; }

        public virtual int GearUp() {
            if (Gear != MaxGear) {
                ++Gear;
            }
            return Gear;
        }

        public virtual int GearDown() {
            if (Gear > 0) {
                --Gear;
            }
            return Gear;
        }

    }

    class Vehicle : VehicleBase {

        public Vehicle(int horsePower, int? maxGear) : base(horsePower, maxGear) { }

        private int _gear;
        public override int Gear {
            get => _gear;
            set {
                if (HorsePower < 180) {
                    if (Speed >= 0 && Speed <= 40) {
                        _gear = 1;
                    }
                    else if (Speed > 40 && Speed <= 70) {
                        _gear = 2;
                    }
                    else if (Speed > 70 && Speed <= 80) {
                        _gear = 3;
                    }
                    else if (Speed > 80 && Speed <= 100) {
                        _gear = 4;
                    }
                    else {
                        _gear = MaxGear;
                    }
                }
                else {
                    if (Speed >= 0 && Speed <= 80) {
                        _gear = 1;
                    }
                    else if (Speed > 80 && Speed <= 120) {
                        _gear = 2;
                    }
                    else if (Speed > 120 && Speed <= 180) {
                        _gear = 3;
                    }
                    else if (Speed > 180 && Speed <= 220) {
                        _gear = 4;
                    }
                    else {
                        _gear = MaxGear;
                    }
                }
            }
        }
    }

    class Car : Vehicle {
        public Car(int horsePower, int? maxGear = 6) : base(horsePower, maxGear) {
            if (horsePower < 30) {
                throw new ApplicationException("Cannot be sent below 30 hp");
            }

            if (maxGear < 2) {
                throw new ApplicationException("Cannot be sent below 2 gear");
            }
        }
    }
}

namespace GurkanTuna.Polymorphism {

    interface IVehicle {
        string Start();
    }

    abstract class VehicleBase : IVehicle {

        public abstract string Move();

        public string Start() {
            return $"The {this.GetType().Name} started";
        }
    }

    interface IFlyable {
        string Fly();
    }

    interface ISwimmable {
        string Swim();
    }


    class Car : VehicleBase {
        public override string Move() {
            return $"The {this.GetType().Name} is moving on the highway";
        }
    }

    class Boat : VehicleBase, ISwimmable {
        public override string Move() {
            return $"The {this.GetType().Name} is moving in the sea";
        }

        public string Swim() {
            return $"The {this.GetType().Name} is swimming...";
        }
    }

    class Plane : VehicleBase, IFlyable {
        public string Fly() {
            return $"The {this.GetType().Name} is flying...";
        }

        public override string Move() {
            return $"The {this.GetType().Name} is moving on the runway";
        }
    }
}