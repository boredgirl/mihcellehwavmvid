﻿using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTabs
{
    public partial class TabContainerBase : ComponentBase
    {

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<TabEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<TabEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<TabEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<TabEvent> HiddenEvent { get; set; }
        [Parameter] public EventCallback<TabEvent> AddedEvent { get; set; }
        [Parameter] public EventCallback<TabEvent> RemovedEvent { get; set; }
        [Parameter] public string ElementId { get; set; }

        private bool Disposing { get; set; } = false;
        private bool HasRendered { get; set; } = false;
        private List<EventCallback<TabEvent>> TabEvents { get; set; } = new List<EventCallback<TabEvent>>();
        private TabEvent TabEvent { get; set; }

        private ITabItem _activeTab { get; set; }
        public ITabItem ActiveTab
        {
            get
            {
                return _activeTab;
            }
            set
            {
                if (this.ActiveTab == value) return;
                if (this.Disposing) return;
                this.TabEvent = new TabEvent() { ActivatedItem = value, DeactivatedItem = _activeTab };

                if (this.HasRendered)
                {
                    InvokeAsync(() => ShowEvent.InvokeAsync(this.TabEvent));
                    InvokeAsync(() => HideEvent.InvokeAsync(this.TabEvent));
                    this.TabEvents.Add(this.ShownEvent);
                    this.TabEvents.Add(this.HiddenEvent);
                }
                this._activeTab = value;
                InvokeAsync(StateHasChanged);
            }
        }

        public List<ITabItem> TabItems { get; set; } = new List<ITabItem>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if(this.ActiveTab == null && this.TabItems.Any())
                {
                    this.ActiveTab = this.TabItems.FirstOrDefault();
                }
                this.HasRendered = true;
            }

            for (var i = 0; i < this.TabEvents.Count; i++)
            {
                await this.TabEvents[i].InvokeAsync(this.TabEvent);
                this.TabEvents.RemoveAt(i);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void SelectTabById(int id)
        {
            this.ActiveTab = this.TabItems.Find(item => item.Id == id);
        }

        public void AddTabItem(ITabItem tabItem)
        {
            if (!TabItems.Any(item => item.Id == tabItem.Id))
            {
                InvokeAsync(() =>
                {
                    this.TabItems.Add(tabItem);
                    this.TabEvents.Add(AddedEvent);

                    StateHasChanged();
                });
            }
        }

        public void RemoveTabItem(int id)
        {
            var tabItem = this.TabItems.Where(item => item.Id == id).FirstOrDefault();
            if (tabItem != null)
            {
                this.TabItems.Remove(tabItem);
                this.TabEvents.Add(RemovedEvent);
            }
        }

        public void ActivateTabEventExecute(object sender, int id)
        {
            var tab = this.TabItems.Find(item => item.Id == id);
            if (tab != null)
            {
                this.ActiveTab = tab;
                StateHasChanged();
            }
        }

    }
}
