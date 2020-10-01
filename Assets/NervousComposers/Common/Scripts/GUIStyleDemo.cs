using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Jusw85.Common
{
    public class GUIStyleDemo : MonoBehaviour
    {
        [SerializeField] private bool showText;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GUIStyleDemo))]
    [CanEditMultipleObjects]
    public class GUIStyleDemoEditor : Editor
    {
        private static string[] styles =
        {
            "box", "button", "toggle", "label", "window", "textfield", "textarea", "horizontalslider",
            "horizontalsliderthumb", "verticalslider", "verticalsliderthumb", "horizontalscrollbar",
            "horizontalscrollbarthumb", "horizontalscrollbarleftbutton", "horizontalscrollbarrightbutton",
            "verticalscrollbar", "verticalscrollbarthumb", "verticalscrollbarupbutton", "verticalscrollbardownbutton",
            "scrollview", "AboutWIndowLicenseLabel", "AC BoldHeader", "AC Button", "AC ComponentButton",
            "AC GroupButton",
            "AC LeftArrow", "AC PreviewHeader", "AC PreviewText", "AC RightArrow", "AM ChannelStripHeaderStyle",
            "AM EffectName", "AM HeaderStyle", "AM MixerHeader", "AM MixerHeader2", "AM ToolbarLabel",
            "AM ToolbarObjectField", "AM TotalVuLabel", "AM VuValue", "AnimationEventBackground",
            "AnimationEventTooltip",
            "AnimationEventTooltipArrow", "AnimationKeyframeBackground", "AnimationPlayHead", "AnimationRowEven",
            "AnimationRowOdd", "AnimationSelectionTextField", "AnimationTimelineTick", "AnimPropDropdown", "AppToolbar",
            "AppToolbarButtonLeft", "AppToolbarButtonMid", "AppToolbarButtonRight", "ArrowNavigationLeft",
            "ArrowNavigationRight", "AssetLabel", "AssetLabel Icon", "AssetLabel Partial", "AvatarMappingBox",
            "AvatarMappingErrorLabel", "AxisLabelNumberField", "Badge", "BoldLabel", "BoldToggle",
            "BottomShadowInwards",
            "BreadcrumbsSeparator", "ButtonLeft", "ButtonMid", "ButtonRight", "BypassToggle", "CacheFolderLocation",
            "CenteredLabel", "ChannelStripAttenuationBar", "ChannelStripAttenuationMarkerSquare", "ChannelStripBg",
            "ChannelStripDuckingMarker", "ChannelStripEffectBar", "ChannelStripSendReturnBar", "ChannelStripVUMeterBg",
            "CircularToggle", "CN Box", "CN CenteredText", "CN CountBadge", "CN EntryBackEven", "CN EntryBackOdd",
            "CN EntryError", "CN EntryErrorIcon", "CN EntryErrorIconSmall", "CN EntryErrorSmall", "CN EntryInfo",
            "CN EntryInfoIcon", "CN EntryInfoIconSmall", "CN EntryInfoSmall", "CN EntryWarn", "CN EntryWarnIcon",
            "CN EntryWarnIconSmall", "CN EntryWarnSmall", "CN Message", "CN StacktraceBackground", "CN StacktraceStyle",
            "CN StatusError", "CN StatusInfo", "CN StatusWarn", "Color.Selected", "ColorField", "ColorPicker2DThumb",
            "ColorPickerBackground", "ColorPickerBox", "ColorPickerCurrentColor",
            "ColorPickerCurrentExposureSwatchBorder",
            "ColorPickerExposureSwatch", "ColorPickerHorizThumb", "ColorPickerHueRing", "ColorPickerHueRing HDR",
            "ColorPickerHueRingThumb", "ColorPickerOriginalColor", "ColorPickerSliderBackground", "Command",
            "CommandLeft",
            "CommandMid", "CommandRight", "ControlHighlight", "ControlLabel", "CurveEditorBackground",
            "CurveEditorLabelTickmarks", "CurveEditorLabelTickmarksOverflow", "CurveEditorRightAlignedLabel",
            "DD HeaderStyle", "DD ItemCheckmark", "DD ItemStyle", "DD LargeItemStyle", "DefaultCenteredLargeText",
            "DefaultCenteredText", "DefaultLineSeparator", "dockarea", "dockareaOverlay", "dockareaStandalone",
            "DopesheetBackground", "Dopesheetkeyframe", "DopesheetScaleLeft", "DopesheetScaleRight", "dragtab",
            "dragtabdropwindow", "DropDown", "DropDownButton", "DropzoneStyle", "EditModeSingleButton",
            "EditModeToolbar",
            "EditModeToolbarLeft", "EditModeToolbarMid", "EditModeToolbarRight", "ErrorLabel", "ExposablePopupItem",
            "ExposablePopupMenu", "EyeDropperHorizontalLine", "EyeDropperPickedPixel", "EyeDropperVerticalLine",
            "flow background", "flow node 0", "flow node 0 on", "flow node 1", "flow node 1 on", "flow node 2",
            "flow node 2 on", "flow node 3", "flow node 3 on", "flow node 4", "flow node 4 on", "flow node 5",
            "flow node 5 on", "flow node 6", "flow node 6 on", "flow node base", "flow node hex 0",
            "flow node hex 0 on",
            "flow node hex 1", "flow node hex 1 on", "flow node hex 2", "flow node hex 2 on", "flow node hex 3",
            "flow node hex 3 on", "flow node hex 4", "flow node hex 4 on", "flow node hex 5", "flow node hex 5 on",
            "flow node hex 6", "flow node hex 6 on", "flow node hex base", "flow node titlebar", "flow target in",
            "flow triggerPin in", "flow triggerPin out", "flow varPin in", "flow varPin out", "flow varPin tooltip",
            "Foldout", "FoldoutHeader", "FoldoutHeaderIcon", "FoldOutPreDrop", "Font.Clip", "GameViewBackground",
            "Grad Down Swatch", "Grad Down Swatch Overlay", "Grad Up Swatch", "Grad Up Swatch Overlay", "grey_border",
            "GridList", "GridListText", "groupBackground", "GroupBox", "GUIEditor.BreadcrumbLeft",
            "GUIEditor.BreadcrumbMid", "GV Gizmo DropDown", "HeaderLabel", "HelpBox", "Hi Label",
            "HorizontalMinMaxScrollbarThumb", "hostview", "Icon.Activation", "Icon.AutoKey", "Icon.AvatarMaskOff",
            "Icon.AvatarMaskOn", "Icon.BlendEaseIn", "Icon.BlendEaseOut", "Icon.BlendMixIn", "Icon.BlendMixOut",
            "Icon.Clip", "Icon.ClipIn", "Icon.ClipOut", "Icon.ClipSelected", "Icon.Connector", "Icon.Curves",
            "Icon.Endmarker", "Icon.ExtrapolationContinue", "Icon.ExtrapolationHold", "Icon.ExtrapolationLoop",
            "Icon.ExtrapolationPingPong", "Icon.Foldout", "Icon.InfiniteTrack", "Icon.Keyframe", "Icon.Locked",
            "Icon.LockedBG", "Icon.Mute", "Icon.Options", "Icon.OutlineBorder", "Icon.PlayAreaEnd",
            "Icon.PlayAreaStart",
            "Icon.Playrange", "Icon.Shadow", "Icon.TimeCursor", "Icon.TrackHeaderSwatch", "Icon.TrackOptions",
            "Icon.Warning", "IconButton", "IN BigTitle", "IN BigTitle Inner", "IN BigTitle Post", "IN CenteredLabel",
            "IN DropDown", "IN EditColliderButton", "IN Foldout", "IN Label", "IN LockButton", "IN MinMaxStateDropDown",
            "IN ObjectField", "IN TextField", "IN ThumbnailSelection", "IN ThumbnailShadow", "IN Title", "IN TitleText",
            "IN TypeSelection", "InnerShadowBg", "InsertionMarker", "InvisibleButton", "LargeBoldLabel", "LargeButton",
            "LargeButtonLeft", "LargeButtonMid", "LargeButtonRight", "LargeLabel", "LightmapEditorSelectedHighlight",
            "LockedHeaderButton", "LODBlackBox", "LODCameraLine", "LODLevelNotifyText", "LODRendererAddButton",
            "LODRendererButton", "LODRendererRemove", "LODRenderersText", "LODSceneText", "LODSliderBG",
            "LODSliderRange",
            "LODSliderRangeSelected", "LODSliderText", "LODSliderTextSelected", "MarkerItem", "MarkerMultiOverlay",
            "MeBlendBackground", "MeBlendPosition", "MeBlendTriangleLeft", "MeBlendTriangleRight",
            "MeLivePlayBackground",
            "MeLivePlayBar", "MenuItem", "MenuItemMixed", "MeTimeBlockLeft", "MeTimeBlockRight", "MeTimeLabel",
            "MeTransitionBack", "MeTransitionBlock", "MeTransitionHandleLeft", "MeTransitionHandleLeftPrev",
            "MeTransitionHandleRight", "MeTransitionHead", "MeTransitionSelect", "MeTransitionSelectHead",
            "MeTransOff2On",
            "MeTransOffLeft", "MeTransOffRight", "MeTransOn2Off", "MeTransOnLeft", "MeTransOnRight", "MeTransPlayhead",
            "MiniBoldLabel", "minibutton", "minibuttonleft", "minibuttonmid", "minibuttonright", "MiniLabel",
            "MiniMinMaxSliderHorizontal", "MiniMinMaxSliderVertical", "MiniPopup", "MiniPullDown",
            "MiniSliderHorizontal",
            "MiniSliderVertical", "MiniTextField", "MiniToolbarButton", "MiniToolbarButtonLeft",
            "MinMaxHorizontalSliderThumb", "MultiColumnArrow", "MultiColumnHeader", "MultiColumnHeaderCenter",
            "MultiColumnHeaderRight", "MultiColumnTopBar", "MuteToggle", "NotificationBackground", "NotificationText",
            "ObjectField", "ObjectFieldMiniThumb", "ObjectFieldThumb", "ObjectFieldThumbLightmapPreviewOverlay",
            "ObjectFieldThumbOverlay", "ObjectFieldThumbOverlay2", "ObjectPickerBackground", "ObjectPickerLargeStatus",
            "ObjectPickerPreviewBackground", "ObjectPickerResultsEven", "ObjectPickerResultsGrid",
            "ObjectPickerResultsOdd",
            "ObjectPickerSmallStatus", "ObjectPickerTab", "ObjectPickerToolbar", "OffsetDropDown", "OL box",
            "OL box NoExpand", "OL EntryBackEven", "OL EntryBackOdd", "OL Label", "OL MiniPing", "OL MiniRenameField",
            "OL Minus", "OL Ping", "OL Plus", "OL ResultFocusMarker", "OL ResultLabel", "OL RightLabel",
            "OL SelectedRow",
            "OL Title", "OL Title TextRight", "OL Toggle", "OL ToggleMixed", "OL ToggleWhite", "OT BottomBar",
            "OT TopBar",
            "OverrideMargin", "PaneOptions", "PlayerSettingsLevel", "PlayerSettingsPlatform", "Popup",
            "PopupCurveDropdown",
            "PopupCurveEditorBackground", "PopupCurveEditorSwatch", "PopupCurveSwatchBackground",
            "PR BrokenPrefabLabel",
            "PR DisabledBrokenPrefabLabel", "PR DisabledLabel", "PR DisabledPrefabLabel", "PR Insertion", "PR Label",
            "PR Ping", "PR PrefabLabel", "PR TextField", "PreBackground", "PreBackgroundSolid", "PreButton",
            "PreButtonBlue", "PreButtonGreen", "PreButtonRed", "PreDropDown", "PreferencesKeysElement",
            "PreferencesSection", "PreferencesSectionBox", "PreHorizontalScrollbar", "PreHorizontalScrollbarThumb",
            "PreLabel", "PreLabelUpper", "PreMiniLabel", "PreOverlayLabel", "PreSlider", "PreSliderThumb", "PreToolbar",
            "PreToolbar2", "PreVerticalScrollbar", "PreVerticalScrollbarThumb", "ProfilerBadge",
            "ProfilerGraphBackground",
            "ProfilerLeftPane", "ProfilerNoDataAvailable", "ProfilerPaneSubLabel", "ProfilerRightPane",
            "ProfilerScrollviewBackground", "ProfilerSelectedLabel", "ProfilerTimelineBar",
            "ProfilerTimelineDigDownArrow",
            "ProfilerTimelineFoldout", "ProfilerTimelineLeftPane", "ProfilerTimelineRollUpArrow", "ProgressBarBack",
            "ProgressBarBar", "ProgressBarText", "ProjectBrowserBottomBarBg", "ProjectBrowserGridLabel",
            "ProjectBrowserHeaderBgMiddle", "ProjectBrowserHeaderBgTop", "ProjectBrowserIconAreaBg",
            "ProjectBrowserIconDropShadow", "ProjectBrowserPreviewBg", "ProjectBrowserSubAssetBg",
            "ProjectBrowserSubAssetBgCloseEnded", "ProjectBrowserSubAssetBgDivider", "ProjectBrowserSubAssetBgMiddle",
            "ProjectBrowserSubAssetBgOpenEnded", "ProjectBrowserSubAssetExpandBtn",
            "ProjectBrowserTextureIconDropShadow",
            "ProjectBrowserTopBarBg", "QualitySettingsDefault", "Radio", "RectangleToolHBar", "RectangleToolHBarLeft",
            "RectangleToolHBarRight", "RectangleToolHighlight", "RectangleToolScaleBottom", "RectangleToolScaleLeft",
            "RectangleToolScaleRight", "RectangleToolScaleTop", "RectangleToolSelection", "RectangleToolVBar",
            "RectangleToolVBarBottom", "RectangleToolVBarTop", "RegionBg", "ReorderableList",
            "ReorderableListRightAligned",
            "RightAlignedLabel", "RightLabel", "RL Background", "RL DragHandle", "RL Element", "RL Footer",
            "RL FooterButton", "RL Header", "SC ViewAxisLabel", "SC ViewLabel", "SC ViewLabelCentered",
            "SC ViewLabelLeftAligned", "SceneTopBarBg", "SceneViewOverlayTransparentBackground", "SceneVisibility",
            "ScriptText", "SearchCancelButton", "SearchCancelButtonEmpty", "SearchModeFilter", "SearchTextField",
            "SelectionRect", "sequenceClip", "sequenceGroupFont", "sequenceTrackHeaderFont", "SettingsHeader",
            "SettingsIconButton", "SettingsListItem", "SettingsTreeItem", "ShurikenCheckMark", "ShurikenCheckMarkMixed",
            "ShurikenDropdown", "ShurikenEditableLabel", "ShurikenEffectBg", "ShurikenEmitterTitle", "ShurikenLabel",
            "ShurikenMinus", "ShurikenModuleBg", "ShurikenModuleTitle", "ShurikenObjectField", "ShurikenPlus",
            "ShurikenPopup", "ShurikenToggle", "ShurikenToggleMixed", "ShurikenValue", "SignalEmitter", "SliderMixed",
            "SoloToggle", "StaticDropdown", "sv_iconselector_back", "sv_iconselector_button",
            "sv_iconselector_labelselection", "sv_iconselector_selection", "sv_iconselector_sep", "sv_label_0",
            "sv_label_1", "sv_label_2", "sv_label_3", "sv_label_4", "sv_label_5", "sv_label_6", "sv_label_7",
            "TabWindowBackground", "Tag MenuItem", "TE BoxBackground", "TE DefaultTime", "TE DropField",
            "TE ElementBackground", "TE NodeBackground", "TE NodeBox", "TE NodeBoxSelected", "TE NodeLabelBot",
            "TE NodeLabelTop", "TE PinLabel", "TE Toolbar", "TE toolbarbutton", "TE ToolbarDropDown",
            "TextFieldDropDown",
            "TextFieldDropDownText", "TimeAreaToolbar", "TimeScrubber", "TimeScrubberButton", "tinyFont", "TL InPoint",
            "TL OutPoint", "TL Playhead", "ToggleMixed", "Toolbar", "toolbarbutton", "ToolbarButtonFlat",
            "ToolbarDropDown",
            "ToolbarPopup", "ToolbarSeachCancelButton", "ToolbarSeachCancelButtonEmpty", "ToolbarSeachTextField",
            "ToolbarSeachTextFieldPopup", "ToolbarSearchField", "ToolbarTextField", "Tooltip",
            "TrackCollapseMarkerButton",
            "TV Insertion", "TV Line", "TV LineBold", "TV Ping", "TV Selection", "U2D.createRect", "U2D.dragDot",
            "U2D.dragDotActive", "U2D.dragDotDimmed", "U2D.pivotDot", "U2D.pivotDotActive", "VCS_StickyNote",
            "VCS_StickyNoteArrow", "VCS_StickyNoteLabel", "VCS_StickyNoteP4", "VerticalMinMaxScrollbarThumb",
            "VideoClipImporterLabel", "WarningOverlay", "WhiteBackground", "WhiteBoldLabel", "WhiteLabel",
            "WhiteLargeCenterLabel", "WhiteLargeLabel", "WhiteMiniLabel", "WinBtn", "WinBtnClose", "WinBtnCloseMac",
            "WinBtnInactiveMac", "WinBtnMax", "WinBtnMaxMac", "WinBtnMinMac", "WinBtnRestore", "WinBtnRestoreMac",
            "WindowBottomResize", "Wizard Box", "Wizard Error", "WordWrapLabel", "wordwrapminibutton",
            "WordWrappedLabel",
            "WordWrappedMiniLabel"
        };

        private static GUIStyle[] guiStyles;
        private static ImagePosition[] imagePositions;

        private SerializedProperty showTextProp;

        private void OnEnable()
        {
            showTextProp = serializedObject.FindProperty("showText");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            showTextProp.boolValue = EditorGUILayout.Toggle("Show Text", showTextProp.boolValue);
            EditorGUILayout.Space();
            if (guiStyles == null)
            {
                // guiStyles = styles.Select(s => new GUIStyle(GUI.skin.GetStyle(s))).ToArray();
                Dictionary<string,GUIStyle> dict = GetStyleList();
                styles = dict.Keys.ToArray();
                guiStyles = dict.Values.Select(s => new GUIStyle(s)).ToArray();
                imagePositions = guiStyles.Select(s => s.imagePosition).ToArray();
            }

            for (int i = 0; i < guiStyles.Length; i++)
            {
                GUIStyle guiStyle = guiStyles[i];
                guiStyle.imagePosition = showTextProp.boolValue ? imagePositions[i] : ImagePosition.ImageOnly;
                EditorGUILayout.TextField(styles[i], "text", guiStyle);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private static Dictionary<string, GUIStyle> GetStyleList()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = typeof(GUISkin).GetField("m_Styles", flags);
            return (Dictionary<string, GUIStyle>) field.GetValue(GUI.skin);
        }
    }
#endif
}