using System.Text;
using Umbraco.Web.PublishedModels;

namespace JB.UmbracoExtensions
{
    public static class SectionEffectParser
    {
        public static string GetSectionEffectCssClass (this IEcAbstractSectionEffects item) {

            if (!item.AnimationEnable) { return null; }

            StringBuilder sb = new StringBuilder("wow fadeIn");

            if (item.AnimationDirection != null)
            {
                sb.Append(item.AnimationDirection.ToString());
            }
            
            if (item.AnimationLarger)
            {
                sb.Append("Big");
            }

            return (sb.ToString());
        }
    }
}