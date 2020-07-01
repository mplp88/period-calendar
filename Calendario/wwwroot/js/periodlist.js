let periodList = new Vue({
  data() {
    return {
      currentAttr: '',
    }
  },
  methods: {
    async mouseenter(key) {
      let vm = this;
      vm.currentAttr = calendar.attrs.find(x => x.key == key);
      calendar.attrs = calendar.attrs.filter(x => x.key != vm.currentAttr.key);
      vm.currentAttr.highlight = 'yellow';
      vm.highlightAttr();
      const _calendar = calendar.$refs.calendar;
      await _calendar.move(vm.currentAttr.dates.start ? vm.currentAttr.dates.start : vm.currentAttr.dates);
    },
    mouseleave() {
      let vm = this;
      calendar.attrs = calendar.attrs.filter(x => x.key != vm.currentAttr.key);
      vm.currentAttr.highlight = 'pink';
      vm.regenerateToday();
      vm.highlightAttr();
      vm.currentAttr = '';
    },
    highlightAttr() {
      let vm = this;
      let today = calendar.attrs.find(x => x.key == 'today');
      calendar.createAttr(vm.currentAttr.dates, vm.currentAttr.key, vm.currentAttr.highlight, vm.currentAttr.popover);
      if (today) {
        let currDate = vm.currentAttr.dates.start ? new Date(vm.currentAttr.dates.start) : new Date(vm.currentAttr.dates)
        if ((currDate.getDate() == today.dates.getDate() && currDate.getMonth() == today.dates.getMonth() && currDate.getFullYear() == today.dates.getFullYear())) {
          vm.removeToday();
        }
      } else {
        vm.regenerateToday();
      }
    },
    removeToday() {
      calendar.attrs = calendar.attrs.filter(x => x.key != 'today');
    },
    regenerateToday() {
      calendar.createTodayAttr();
    }
  },
  mounted() {}
}).$mount('#period-list')