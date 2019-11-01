using System.Collections.Generic;
using System.Linq;
using Rocket.API.Serialisation;
using Rocket.Core;

namespace SkillSets
{
	public class SkillSet
	{
		public static SkillSet FromConfigurationSkillSet(SkillSetsConfiguration.Configuration_SkillSet confSkillSet) 
		{

			RocketPermissionsGroup group = null;
			List<Skill> skills = confSkillSet.Skills.Select((confSkill) => { return Skill.FromConfigurationSkill(confSkill); }).Where((skill) => skill != null).ToList();
			if (confSkillSet.PermissionGroup != null && confSkillSet.PermissionGroup.Trim().Length > 0)
			{
				group = R.Permissions.GetGroup(confSkillSet.PermissionGroup);
			}
			return new SkillSet(confSkillSet.Name, group, skills, confSkillSet.Default);
		}

		public readonly string Name = string.Empty;
		public readonly RocketPermissionsGroup PermissionGroup;
		public readonly List<Skill> Skills = new List<Skill>();
		public readonly bool IsDefault;

		public int PermissionGroupPriority
		{
			get
			{
				return PermissionGroup != null ? PermissionGroup.Priority : 0;
			}
		}

		internal SkillSet(string name, RocketPermissionsGroup permissionGroup, List<Skill> skills, bool isDefault)
		{
			Name = name;
			PermissionGroup = permissionGroup;
			Skills = skills;
			IsDefault = isDefault;
		}

		public string GetPermissionName()
		{
			return string.Format("skillsets.{0}", Name);
		}

		public int GetScore()
		{
			return Skills.Aggregate(0, (int acc, Skill skill) =>
			{
				return acc + (int)System.Math.Round(((double)skill.Level) / ((double)skill.MaxLevel) * 100);
			});
		}

		public bool IsEmpty() 
		{
			return Skills.Count == 0;
		}

	}
}
