Tessl is intended to be the simplest possible service locator to make a component with dependencies testable. This is a C# implementation.

Tessl has no configuration file and no registry. 
Requires minimal change to production code
Requires minimal effort in test methods

It requires production code like this:

<code>
RequiredType1 dependencyByConstructor         = new RequiredType1();
RequiredType2 dependencyFromFactoryMethod     = factoryMethod(arg1, arg2);
RequiredType3 dependencyFromIbjectInitializer = RequiredType3 { Prop1=value1, Field1=value2 };
</code>

to be replaced by this:

<code>
RequiredType1 dependencyByConstructor         = Tessl.New(RequiredType1);
RequiredType2 dependencyFromFactoryMethod     = Tessl.Build<RequiredType2, ParamType1, ParamType2>( factoryMethod, arg1, arg2);
RequiredType3 dependencyFromIbjectInitializer = Tessl.Init<RequiredType3>( RequiredType3 { Prop1=value1, Field1=value2 });
</code>

with no further type configuration registration.

in test code it is intended to require only this in Test setup:

<code>
Tessl.ConfigurationMode= Tessl.ConfigurationType.Test;
</code>

to cause all dependent types created via Tessl to be mocked out.

If you can help to achieve simplicity, please join in.