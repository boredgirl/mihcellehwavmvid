﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Mihcelle.Hwavmvid.Modules.Roulette.Coins;
using Mihcelle.Hwavmvid.Modules.Roulette.Betoptions;
using Mihcelle.Hwavmvid.Modules.Roulette.Surface;
using Mihcelle.Hwavmvid.Modules.Roulette.Bets;
using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Items;
using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Enums;
using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Events;
using Mihcelle.Hwavmvid.Modules.Roulette.Itellisense;

namespace Mihcelle.Hwavmvid.Modules.Roulette.Itellisense
{
    public class RouletteitellisenseComponentBase : ComponentBase, IAsyncDisposable
    {

        [Inject] public RouletteitellisenseService? RouletteitellisenseService { get; set; }
        [Inject] public RouletteService? RouletteService { get; set; }
        [Inject] public RoulettecoinsService? RoulettecoinsService { get; set; }
        [Inject] public RouletteBetoptionsService? RouletteBetoptionsService { get; set; }
        [Inject] public RoulettesurfaceService? RoulettesurfaceService { get; set; }
        [Inject] public RouletteBetsService? RouletteBetsService { get; set; }

        public RoulettecoinEvent DroppedItem { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.RouletteService.OnStopRouletteGame += GameStopped;
            this.RouletteService.OnWinItemDetected += WinItemDetected;
            this.RoulettecoinsService.OnItemDropped += ItemDropped;
            this.RouletteBetsService.UpdateUI += UpdateUI;
            this.RouletteBetsService.ItemRemoved += BetItemRemoved;

            this.RouletteitellisenseService.ContextGameId = Guid.NewGuid().ToString();
            await base.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    await this.RoulettecoinsService.InitJsMap(this.RouletteBetoptionsService.Items, this.RoulettesurfaceService.NumberItems);
                }
                catch (Exception exception) { Console.WriteLine(exception.Message); }
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        public void ItemDropped(RoulettecoinEvent e)
        {

            var item = new RouletteBetItem()
            {
                Id = Guid.NewGuid().ToString(),
                Coin = e.CoinItem,
                Betoption = e.BetoptionsItem,
                Surfacenumber = e.SurfaceNumber,
            };
            this.RouletteBetsService.AddBetItem(item);

            this.DroppedItem = e;
            this.StateHasChanged();
        }
        public void BetItemRemoved(RouletteBetItem item)
        {
            if (!this.RouletteBetsService.BetItems.Any())
            {
                this.DroppedItem = null;
            }

            this.StateHasChanged();
        }
        public void WinItemDetected(RouletteEvent e)
        {

            foreach (var betitem in this.RouletteBetsService.BetItems)
            {

                if (e.WinItem.Value == 0)
                {
                    this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                    betitem.Status = RouletteBetStatus.Lost;
                }

                if (e.WinItem.Value == 37)
                {
                    this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                    betitem.Status = RouletteBetStatus.Lost;
                }

                if (betitem.Surfacenumber != null)
                {

                    if (betitem.Surfacenumber.Value == e.WinItem.Value)
                    {
                        this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value * 36);
                        betitem.Status = RouletteBetStatus.Won;
                    }
                    else
                    {
                        this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                        betitem.Status = RouletteBetStatus.Lost;
                    }
                }

                if (betitem.Betoption != null)
                {

                    if (betitem.Betoption.Key == RouletteBetoptionsType.Red)
                    {
                        if (e.WinItem.BackgroundColor.ToLower() == betitem.Betoption.Key.ToString().ToLower())
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }
                    }
                    if (betitem.Betoption.Key == RouletteBetoptionsType.Black)
                    {
                        if (e.WinItem.BackgroundColor.ToLower() == betitem.Betoption.Key.ToString().ToLower())
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }
                    }

                    if (betitem.Betoption.Key == RouletteBetoptionsType.FirstHalf)
                    {
                        if (e.WinItem.Value >= 1 && e.WinItem.Value <= 18)
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }

                    }
                    if (betitem.Betoption.Key == RouletteBetoptionsType.SecondTwelve)
                    {
                        if (e.WinItem.Value >= 19 && e.WinItem.Value <= 36)
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }
                    }

                    if (betitem.Betoption.Key == RouletteBetoptionsType.FirstTwelve)
                    {
                        if (e.WinItem.Value >= 1 && e.WinItem.Value <= 12)
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value * 3);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }

                    }
                    if (betitem.Betoption.Key == RouletteBetoptionsType.SecondTwelve)
                    {
                        if (e.WinItem.Value >= 13 && e.WinItem.Value <= 24)
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value * 3);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }

                    }
                    if (betitem.Betoption.Key == RouletteBetoptionsType.ThirdTwelve)
                    {
                        if (e.WinItem.Value >= 25 && e.WinItem.Value <= 36)
                        {
                            this.RouletteitellisenseService.ContextGameValue += (betitem.Coin.Value * 3);
                            betitem.Status = RouletteBetStatus.Won;
                        }
                        else
                        {
                            this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                            betitem.Status = RouletteBetStatus.Lost;
                        }

                    }
                }

                if (betitem.Status == RouletteBetStatus.undefined)
                {
                    this.RouletteitellisenseService.ContextGameValue -= (betitem.Coin.Value);
                    betitem.Status = RouletteBetStatus.Lost;
                }
            }

            this.RoulettesurfaceService.WinItem = e.WinItem;
            this.RoulettesurfaceService.InvokeUpdateUI();
            this.UpdateUI();
        }

        private void GameStopped()
        {
            this.RouletteBetsService.BetItems.Clear();
            this.RouletteitellisenseService.ContextGameId = Guid.NewGuid().ToString();
            this.DroppedItem = null;

            this.RoulettesurfaceService.WinItem = null;
            this.RoulettesurfaceService.InvokeUpdateUI();

            this.RouletteService.GameStatus = RouletteGameStatus.StartNewGame;
            this.RouletteService.RemoveRouletteItem(this.RouletteService.ball.RowId, this.RouletteService.ball.ColumnId, this.RouletteService.ball);
            this.RouletteService.ResetVariables();

            this.StateHasChanged();
        }

        public async void UpdateUI()
        {
            await this.InvokeAsync(() =>
            {
                this.StateHasChanged();
            });
        }

        public async ValueTask DisposeAsync()
        {
            await InvokeAsync(() =>
            {
                this.RouletteService.OnPlayNewRouletteGame -= GameStopped;
                this.RouletteService.OnWinItemDetected -= WinItemDetected;
                this.RoulettecoinsService.OnItemDropped -= ItemDropped;
                this.RouletteBetsService.UpdateUI -= UpdateUI;
                this.RouletteBetsService.ItemRemoved -= BetItemRemoved;
            });
        }

    }
}
