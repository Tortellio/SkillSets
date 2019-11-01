using Rocket.API;

namespace SkillSets
{
	public static class PermissionUtils
	{

		public static bool IsPermitted(IRocketPlayer player, string skillsetName)
		{
			SkillSet skillSet = Utils.SkillsUtils.FindSkillSetByName(skillsetName);
			if (skillSet == null) return false;
			return player.HasPermission(skillSet.GetPermissionName());
		}

		public static bool IsPermitted(IRocketPlayer player, SkillSet skillSet)
		{
			return player.HasPermission(skillSet.GetPermissionName());
		}
	}
}
