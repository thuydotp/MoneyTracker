<template>
    <div>      
        <g-chart class="transaction-summary-chart"
          type="PieChart"
          ref="gChart"
          :data="chartData"
          :options="chartOptions"
          :events="chartEvents"
        />
        <div class="balance-container">          
            <span>Balance: </span><b>{{displayedBalance}}</b>
        </div>
              
        <div class="create-new-transaction">
            <button class="btn btn-danger round-button" @click="createTransaction(0)">Add Expense</button>
            <button class="btn btn-success round-button" @click="createTransaction(1)">Add Income</button>
        </div>
    </div>
</template>

<script>
import { GChart } from "vue-google-charts";

export default {
  components: {
    "g-chart": GChart
  },
  props: ["transactions"],
  data: function() {
    return {
      expenseCategories: [],
      chartData: [],
      chartOptions: {
        title: "Transaction Summary",
        pieHole: 0.4,
        legend: "none"
      },
      chartEvents: {
        select: value => {
          const table = this.$refs.gChart.chartObject;
          var selection = table.getSelection();
          for (var i = 0; i < selection.length; i++) {
            var item = selection[i];
            if(item) {
              let categoryID = this.categoryIDs[item.row];
              console.log("You just selected item: ", this.expenseCategories[categoryID]);
            }
          }
        },
        click: (targetID)=>{
          console.log("click event", targetID);
        }
      },
      categoryIDs: []
    };
  },
  computed: {
    displayedBalance() {
      let balance = 0;
      balance = this.transactions
        .map(x => {
          let factor = x.type == 0 ? -1 : 1;
          return x.changeValue * factor;
        })
        .reduce((sum, value) => {
          return (sum += value);
        }, 0);
      return balance;
    }
  },
  watch: {
    transactions: function(val) {
      this.transactions = val;
      this.populateChartData();
    }
  },
  methods: {
    createTransaction(transactionType) {
      this.$emit("create-transaction", transactionType);
    },
    getExpenseGroupByCategory() {
      let expenseCategories = [];
      let expenseItems = this.transactions.filter(x => x.type == 0 && x.changeValue > 0);
      for (const key in expenseItems) {
        if (expenseItems.hasOwnProperty(key)) {
          const expense = expenseItems[key];
          let expenseCategory = expenseCategories[expense.id];
          let value = (expenseCategory ? expenseCategory.changeValue : 0) + expense.changeValue;
          expenseCategories[expense.id] = { categoryName: expense.categoryName, value: value };
        }
      }
      return expenseCategories;
    },
    populateChartData() {
      let chartData = [];
      chartData.push(["Activity", "Dollar per period"]);
      this.expenseCategories = this.getExpenseGroupByCategory();
      let rowIndex = 0;
      for (var key in this.expenseCategories) {
        if (this.expenseCategories.hasOwnProperty(key)) {
          const expense = this.expenseCategories[key];
          chartData.push([expense.categoryName, expense.value]);
          this.categoryIDs[rowIndex] = key;
          rowIndex++;
        }
      }
      this.chartData = chartData;
    }
  },
  async created() {
    this.populateChartData();
  }
};
</script>

<style scoped>
.transaction-summary-chart {
  display: flex;
  justify-content: space-between;
  width: 50%;
  margin: auto;
  height: 400px;
}

.balance-container {
  text-align: center;
  font-size: 30px;
}

.create-new-transaction {
  display: flex;
  justify-content: space-between;
  margin: auto;
  width: 80%;
  max-width: 400px;
}
</style>
