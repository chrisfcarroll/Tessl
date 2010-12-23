Tessl is a Testability-focused Extremely Simple Service Locator. This is a C# implementation.

It is intended to provide the simplest possible service locator that can make a component with dependencies testable. It is suitable for:

- Making legacy code with hard-wired dependencies testable with minimal change. 
	Hard-wired dependencies can be replaced with tesslated dependencies by a relatively simple search and replace.
- Writing testing code in the absence of a dependency injection framework, and with the least possible configuration - none at all.

How Simple?

It has no configuration file and no registry.
Requires minimal change to production code.
Requires one line of code to setup in a test fixture
Works with your favourite Mocks or with stubs

What will my tesslated production code looke like? Will it first requires hours of rewriting?

Tesslated Constructors
<code>RequiredType1 dependencyByConstructor = new RequiredType1(arg1, arg2);</code>
becomes:
<code>RequiredType1 dependencyByConstructor = Tessl.New<RequiredType1>(arg1, arg2);</code>

Tesslated Factory Methods

<code>RequiredType2 dependencyFromFactoryMethod = FactoryClass.FactoryMethod(arg1, arg2);</code>
becomes:
<code>RequiredType2 dependencyFromFactoryMethod = Tessl.From<ParamType1, ParamType2, RequiredType2>( FactoryClass.FactoryMethod, arg1, arg2);

Tesslated Object Initializers

<code>RequiredType3 dependencyFromObjectInitializer = RequiredType3 { Prop1=value1, Field1=value2 };</code>
becomes:
<code>RequiredType3 dependencyFromIbjectInitializer = Tessl.Init<RequiredType3>( RequiredType3 { Prop1=value1, Field1=value2 });</code>

How do I use it with <MyFavouriteFrameworkForMocks>?

<code>
//Arrange
var mock= MyFavouriteFrameworkForMocks.Mock<ITessl>.Object;
Tessl.Configuration= mock;
mock.Setup( x => x.New<Dependency> ).Return( mockDependency ); //Tessl will injects mock where your pre-tesslated code had a hard-coded constructor call

//Act
var unitUnderTest = new UnitUnderTest();
unitUnderTest.MethodUnderTest();

//Assert
Assert.IsTrue( whateverYouWantedToAssert );
</code>

If you can help to achieve simplicity, please join in.