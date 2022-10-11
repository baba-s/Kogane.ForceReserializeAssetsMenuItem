using System.Linq;
using UnityEditor;

namespace Kogane.Internal
{
    /// <summary>
    /// アセットを再シリアライズするエディタ拡張
    /// </summary>
    internal static class ForceReserializeAssetsMenuItem
    {
        //================================================================================
        // 定数
        //================================================================================
        private const string ITEM_NAME_ROOT = "Kogane/ForceReserializeAssets/";

        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// すべてのアセットを再シリアライズするメニュー
        /// </summary>
        [MenuItem( ITEM_NAME_ROOT + "すべてのアセットを再シリアライズ" )]
        private static void ForceReserializeAssetsAll()
        {
            if ( !OpenOkCancelDialog( "すべてのアセットを再シリアライズしますか？" ) ) return;

            AssetDatabase.ForceReserializeAssets();

            OpenOkDialog( "すべてのアセットを再シリアライズしました" );
        }

        /// <summary>
        /// 選択したフォルダ内のアセットを再シリアライズするメニュー
        /// </summary>
        [MenuItem( ITEM_NAME_ROOT + "選択したフォルダ内のアセットを再シリアライズ" )]
        private static void ForceReserializeAssetsSelect()
        {
            var folderPath = EditorUtility.OpenFolderPanel
            (
                title: "",
                folder: string.Empty,
                defaultName: string.Empty
            );

            folderPath = FileUtil.GetProjectRelativePath( folderPath );

            if ( !OpenOkCancelDialog( $"{folderPath} フォルダ内のアセットを再シリアライズしますか？" ) ) return;

            var folderPathWithSlash = folderPath + "/";

            var list = AssetDatabase
                    .GetAllAssetPaths()
                    .Where( c => c.StartsWith( folderPathWithSlash ) )
                ;

            AssetDatabase.ForceReserializeAssets( list );

            OpenOkDialog( $"{folderPath} フォルダ内のアセットを再シリアライズしました" );
        }

        /// <summary>
        /// OK ダイアログを開きます
        /// </summary>
        private static void OpenOkDialog( string message )
        {
            EditorUtility.DisplayDialog
            (
                title: "",
                message: message,
                ok: "OK"
            );
        }

        /// <summary>
        /// OK キャンセルダイアログを開きます
        /// </summary>
        private static bool OpenOkCancelDialog( string message )
        {
            return EditorUtility.DisplayDialog
            (
                title: "",
                message: message,
                ok: "はい",
                cancel: "いいえ"
            );
        }
    }
}