using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XCore
{
    public enum TaskId
    {
        Task0_SupportMaterial_1_1_Up = 0,
        Task1_SupportMaterial_1_1_Down,
        Task2_SupportMaterial_1_2_Up,
        Task3_SupportMaterial_1_2_Down,
        Task4_SupportMaterial_2_1_Up,
        Task5_SupportMaterial_2_1_Down,
        Task6_SupportMaterial_2_2_Up,
        Task7_SupportMaterial_2_2_Down,

        Task10_Carrier_FeedIn = 10,
        Task11_Carrier_Assemble_NIO,
        Task12_Carrier_Transfer,
        Task13_Carrier_Assemble_RIO,
        Task14_Carrier_FeedOut,

        Task20_FeedMaterial_1_1 = 20,
        Task21_FeedMaterial_1_2,
        Task22_FeedMaterial_2_1,
        Task23_FeedMaterial_2_2,

        Task30_LeftAssemblyMaterial = 30,
        Task31_RightAssemblyMaterial,

        Task40_FlowLine = 40,
        Task41_SupportMaterial,
        Task70_ScannerBox = 70,
    }
}

