using System;
using System.Reflection;

namespace MoreBosses
{
    public enum RegistrationPriority
    {
        // TODO Renmae Enums to specify boss, enemy, shard enhanced enemy, scenario
        CLAN = 0,
        STARTER_RELIC,
        STARTER_CARD,
        CHAMPION,
        SUBTYPE,
        CLAN_BANNER,
        HIGH,
        MEDIUM,
        LOW,
        SCENARIO,
    }

    /// <summary>
    /// Registerable Type.
    /// 
    /// To use simply have a class with a public static AutoRegister() method.
    /// To fine tune when the object is registered add `public static RegistrationPriority Priority = Priority` in the class.
    /// To do some additional work after the object is registered you can add a public static PostRegister() method.
    /// </summary>
    struct RegisterableType
    {
        public Type type;
        public int priority;
        public MethodInfo registerMethod;
        public MethodInfo postRegisterMethod;
    }
}
