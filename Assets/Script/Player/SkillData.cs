using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using static Constants;
public class SkillData : MonoBehaviour
{
    public JObject fireData = new()
    {
        [BASE_COOLDOWN] = 500,
        [BASE_PROJECTILE_SPEED] = 20
    };
}
