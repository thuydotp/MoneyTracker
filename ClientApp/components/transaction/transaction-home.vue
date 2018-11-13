<template>
    <div>
        <h3>Transaction Page</h3>
        <button @click="toggleShowSummary">Toggle transaction Summary</button>
        <transaction-summary v-if="showSummary"></transaction-summary>
        <transaction-item v-else></transaction-item>
    </div>
</template>

<script>
import SpendingSummary from "./spending-summary";
import SpendingChange from "./spending-change";
export default {
  components: {
    "transaction-summary": SpendingSummary,
    "transaction-item": SpendingChange
  },
  data: function(){
    return {
      showSummary: true
    }
  },
  methods: {
    toggleShowSummary() {
      this.showSummary = !this.showSummary;
    }
  }
};
</script>
