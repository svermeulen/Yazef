using System;
using UnityEngine;

namespace Zenject
{
    public enum ValidationErrorResponses
    {
        Log,
        Throw
    }

    public enum RootResolveMethods
    {
        NonLazyOnly,
        All
    }

    [Serializable]
    [ZenjectAllowDuringValidation]
    public class ZenjectSettings
    {
        public static ZenjectSettings Default = new ZenjectSettings();

        [SerializeField]
        bool _ensureDeterministicDestructionOrderOnApplicationQuit;

        [SerializeField]
        bool _displayWarningWhenResolvingDuringInstall;

        [SerializeField]
        RootResolveMethods _validationRootResolveMethod;

        [SerializeField]
        ValidationErrorResponses _validationErrorResponse;

        [SerializeField]
        ConstructorChoiceStrategy _constructorChoiceStrategy;

        [SerializeField]
        public ZenjectSettings(
            ValidationErrorResponses validationErrorResponse,
            RootResolveMethods validationRootResolveMethod = RootResolveMethods.NonLazyOnly,
            bool displayWarningWhenResolvingDuringInstall = true,
            bool ensureDeterministicDestructionOrderOnApplicationQuit = false,
            ConstructorChoiceStrategy constructorChoiceStrategy = ConstructorChoiceStrategy.InjectAttributeThenLeastArguments)
        {
            _validationErrorResponse = validationErrorResponse;
            _validationRootResolveMethod = validationRootResolveMethod;
            _displayWarningWhenResolvingDuringInstall = displayWarningWhenResolvingDuringInstall;
            _ensureDeterministicDestructionOrderOnApplicationQuit =ensureDeterministicDestructionOrderOnApplicationQuit;
            _constructorChoiceStrategy = constructorChoiceStrategy;
        }

        // Need to define an emtpy constructor since this is created by unity serialization
        // even if the above constructor has defaults for all
        public ZenjectSettings()
            : this(ValidationErrorResponses.Log)
        {
        }

        public ConstructorChoiceStrategy ConstructorChoiceStrategy
        {
            get { return _constructorChoiceStrategy; }
        }

        // Setting this to Log can be more useful because it will print out
        // multiple validation errors at once so you can fix multiple problems before
        // attempting validation again
        public ValidationErrorResponses ValidationErrorResponse
        {
            get { return _validationErrorResponse; }
        }

        // Settings this to true will ensure that every binding in the container can be
        // instantiated with all its dependencies, and not just those bindings that will be
        // constructed as part of the object graph generated from the nonlazy bindings
        public RootResolveMethods ValidationRootResolveMethod
        {
            get { return _validationRootResolveMethod; }
        }

        public bool DisplayWarningWhenResolvingDuringInstall
        {
            get { return _displayWarningWhenResolvingDuringInstall; }
        }

        // When this is set to true and the application is exitted, all the scenes will be
        // destroyed in the reverse order in which they were loaded, and then the project context
        // will be destroyed last
        // When this is set to false (the default) the order that this occurs in is not predictable
        // It is set to false by default because manually destroying objects during OnApplicationQuit
        // event can cause crashes on android (see github issue #468)
        public bool EnsureDeterministicDestructionOrderOnApplicationQuit
        {
            get { return _ensureDeterministicDestructionOrderOnApplicationQuit; }
        }
    }
}
