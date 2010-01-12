
MSpec is designed to reduce noise in tests.

MSpec.Contrib tries to take this a step further by reducing noise in creating mock objects, dependencies and the subject under test.

It draws heavily (steals from :))JP Boodhoo's developwithpassion.dll library for the abstractions around the mocking framework (RhinoMocks) and the guts of the subject and dependency builders.

Features:

Three base specification classes:
	SpecificationForSubjectWithAContract<Contract, Subject>
	SpecificationForSubjectWithoutAContract<Subject>
	SpecficationForStaticSubject

Exposing the following helper methods:

	protected static InterfaceType an<InterfaceType>() where InterfaceType : class

	 - Creates and returns a mock instance of the specified interface, using the underlying mocking framework

	protected static Dependency dependency_of<Dependency>() where Dependency : class
	
	 - Creates a mock instance of the specified interface and adds it to the dependency bag used to create the subject

	protected virtual Contract create_subject()
	 
	 - Allows overriding the subject creation in the specifications if necessary
	
And exposes the following properties:

	protected static Contract subject
	
	Gives direct access to the subject, based on the generic parameter specified in the base specification. The subject 		is created automatically the first time it is accessed, including all registered dependencies.

With ReSharper Live Templates for the three base specification classes.