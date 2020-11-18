using System;
using System.Runtime.InteropServices;

public class HeadDLLImport 
{
    //////////////////////////////////////////////////////////////////////////
    // 换脸
    //////////////////////////////////////////////////////////////////////////
    [DllImport("libHead")] public static extern IntPtr AI_system_init_swaphead(string model_path);
    [DllImport("libHead")] public static extern int AI_swap_head(IntPtr context, float[] head_20481_data, float[] head_swapped);

    //------------------------------- 眼珠 -------------------------------
    //初始化系统，读取眼珠模型，准备数据
    [DllImport("libHead")] public static extern int AI_system_init_eyeball(string eyeball_path);


    //传入人头数据，计算眼珠位置
    //输入的肩膀类型： 0：女（非对称），  1：xueer（对称）  ，2：std 男（对称）
    //注意输入float数组的排列是： xxxxxxxx........ yyyyyyyy........... zzzzzzz...........
    [DllImport("libHead")]
    public static extern int AI_add_eyeball(float[] head_20481_datas, int shoulder_type,
        double[] out_euler_angles_left, double[] out_euler_angles_right,
        double[] out_left_translantion, double[] out_right_translantion,
        ref double out_scale);

    [DllImport("libHead")] public static extern IntPtr AI_system_init_eyeball_qsmy(string eyeball_path);
    [DllImport("libHead")] public static extern IntPtr AI_system_init_eyelash_qsmy(string eyelash_path);


    [DllImport("libHead")] public static extern int AI_add_eyeball_qsmy(IntPtr context, float[] head_20481_data, float[] eyeball_aligned);
    [DllImport("libHead")] public static extern int AI_add_eyelash_qsmy(IntPtr context, float[] head_20481_data, float[] eyelash_aligned);



    //------------------------------- TBN -------------------------------
    //初始化系统，读取TBN 20481相关的模型，准备数据
    [DllImport("libHead")] public static extern int AI_system_init_TBN(string TBN_path);

    [DllImport("libHead")] public static extern int AI_get_TBN(float[] head_20481_datas, float[] out_TBN);




    //---------------------------General TBN------------------------------------------------
   
    [DllImport("libHead")] public static extern IntPtr AI_system_init_TBN_g2(string headobj, string idx_xueer2head_file, string idx_head2ue4_file);
    [DllImport("libHead")] public static extern int AI_get_TBN_g(IntPtr context, float[] head_20481_datas, float[] out_TBN);

    //------------------------------- dynamic BS-------------------------------
    [DllImport("libHead")] public static extern int AI_system_initDynamic_BS(string model_path);
    [DllImport("libHead")] public static extern int AI_get_BS(float[] head_20481_datas, float[] out_BS);
}

