let calendar = new Vue({
  data() {
    return {
      locale: {
        id: 'es',
        firstDayOfWeek: 0,
        masks: {
          weekdays: 'WWW'
        }
      },
      mode: 'single',
      start: null,
      end: null,
      attrs: [],
      showWarning: false
    }
  },
  methods: {
    submitDates() {
      let vm = this;
      vm.doClearDates();
      let start = vm.isValidDate(vm.start) ? vm.start : null;
      let end = vm.isValidDate(vm.end) ? vm.end : null;

      if ((start && end) && (end > start)) {
        vm.createAttr({
          start,
          end
        }, 'periodo', 'pink', 'Periodo')

        $('#Inicio').val(start.toISOString());
        $('#Fin').val(end.toISOString());
        vm.createTodayAttr();
        return;
      }

      if (start) {
        vm.createAttr(start, 'inicio', 'pink', 'Inicio');
        $('#Inicio').val(start.toISOString());
      }

      if (end) {
        vm.createAttr(end, 'fin', 'pink', 'Fin');
        $('#Fin').val(end.toISOString());
      }
      vm.createTodayAttr();
    },
    createTodayAttr() {
      this.createAttr(new Date(), 'today', 'blue', 'Hoy');
    },
    createAttr(dates, key, highlight, popover) {
      let vm = this;
      let attr = {};
      if (key) {
        attr.key = key
      }
      attr.highlight = highlight ? highlight : 'blue';
      attr.popover = popover ? popover.label ? popover : {
        label: popover
      } : null;
      attr.dates = dates;
      vm.attrs.push(attr);
    },
    clearDates() {
      let vm = this;
      vm.showWarning = true;
    },
    doClearDates() {
      let vm = this;
      vm.showWarning = false;
      vm.attrs = [];
    },
    isValidDate(date) {
      return date instanceof Date && !isNaN(date);
    }
  },
  watch: {
    start() {
      let vm = this;
      vm.submitDates();
    },
    end() {
      let vm = this;
      vm.submitDates();
    }
  },
  mounted() {
    let vm = this;
    vm.createTodayAttr();
    let start = $('#Inicio').val();
    vm.start = new Date(start) || null;
    let end = $('#Fin').val();
    vm.end = new Date(end) || null;

  }
}).$mount('#calendar')