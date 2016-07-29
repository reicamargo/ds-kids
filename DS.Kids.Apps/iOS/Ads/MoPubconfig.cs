using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Kids.Apps.iOS.Ads
{
        public class MoPubconfig
        {
#if DEBUG
            public const string AD_UNIT_ID_BANNER = "7e85b8ef70c64a0080b135408da26657";
            public const string AD_UNIT_ID_MEDIUM = "e7b11866a15b415c91b05c78f7a553fd";
            public const string AD_UNIT_ID_FULLSCREEN = "563fcbff74424f3bbe386100df10844f";
#else
        public const string AD_UNIT_ID_BANNER = "8c861a5a02434f768578d0c4491d7534";
        public const string AD_UNIT_ID_SQUARE = "ce345ba2072247e6a515383469012c20";
        public const string AD_UNIT_ID_FULLSCREEN = "6d9e54478e3944db8073e411cfbaf527";
#endif
    }

}
