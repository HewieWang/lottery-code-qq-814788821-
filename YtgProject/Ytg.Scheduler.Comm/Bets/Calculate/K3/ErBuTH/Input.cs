﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.ErBuTH
{
    /// <summary>
    /// 二不同号  输入 2016 01 13
    /// </summary>
    public class Input : BiaoZhun
    {
     

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            var array=item.BetContent.Split('&');
            int count = 0;
            foreach (var a in array)
            {
                if (!(openResult.Contains(a[0]) && openResult.Contains(a[1])))
                    continue;
                count++;
                break;

            }
            if (count>0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT); 
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
         
            return item.BetContent.Split(',').Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = betContent.Replace('&', ',');
            return this.VerificationBetContentOneNum(betContent, 2, 1, 6);
        }
    }
}