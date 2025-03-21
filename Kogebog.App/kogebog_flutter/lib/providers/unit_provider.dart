import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:kogebog_flutter/models/unit.dart';
import 'package:kogebog_flutter/services/unit_service.dart';

class UnitNotifier extends StateNotifier<List<Unit>> {
  final UnitService _unitService;

  UnitNotifier(this._unitService) : super([]) {
    fetchUnits();
  }

  Future<void> fetchUnits() async {
    final units = await _unitService.getAll();
    state = units;
  }
}

final unitProvider = StateNotifierProvider<UnitNotifier, List<Unit>>((ref) {
  final unitService = UnitService();
  return UnitNotifier(unitService);
});
